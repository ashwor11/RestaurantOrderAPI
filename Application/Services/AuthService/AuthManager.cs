using System.IdentityModel.Tokens.Jwt;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Application.Features.Auths.Rules;
using Core.Security.Enums;
using Core.Security.Mailing;
using Core.Security.Vertification.EmailVertification;
using Core.Security.Vertification.OtpVertification;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtpNet;

namespace Application.Services.AuthService
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
        private readonly IEmailVertificatorRepository _emailVertificatorRepository;
        private readonly IOtpVerificatorRepository _otpVerificatorRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IOtpVertificationHelper _otpVertificationHelper;
        private readonly IMailService _mailService;


        public AuthManager(ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IAuthRepository authRepository, IOperationClaimRepository operationClaimRepository, IOptions<TokenValidationParameters> tokenValidationParameters, IRefreshTokenRepository refreshTokenRepository, IEmailAuthenticatorHelper emailAuthenticatorHelper, IEmailVertificatorRepository emailVertificatorRepository, IOtpVerificatorRepository otpVerificatorRepository, AuthBusinessRules authBusinessRules, IOtpVertificationHelper otpVertificationHelper, IMailService mailService)
        {
            _tokenHelper = tokenHelper;
            _userOperationClaimRepository = userOperationClaimRepository;
            _authRepository = authRepository;
            _operationClaimRepository = operationClaimRepository;
            _tokenValidationParameters = tokenValidationParameters.Value;
            _refreshTokenRepository = refreshTokenRepository;
            _emailAuthenticatorHelper = emailAuthenticatorHelper;
            _emailVertificatorRepository = emailVertificatorRepository;
            _otpVerificatorRepository = otpVerificatorRepository;
            _authBusinessRules = authBusinessRules;
            _otpVertificationHelper = otpVertificationHelper;
            _mailService = mailService;
        }

        public bool ConfirmPasswords(Owner owner, string password)
        {
            return HashingHelper.VerifyPasswordHash(password, owner.PasswordHash, owner.PasswordSalt) ? true : throw new BusinessException("Password is not correct.");
        }

        public async Task<AccessToken> CreateAccessToken(Owner owner)
        {
            List<OperationClaim> claims = await GetUserClaims(owner);
            AccessToken token = _tokenHelper.CreateToken(owner, claims);
            return token;
        }

        public async Task<Owner> GetOwnerById(int ownerId)
        {
            return await _authRepository.GetAsync(x => x.Id == ownerId, x => x.Include(x=>x.Restaurants));
        }

        public async Task AddRestaurantClaim(int ownerId, int restaurantId)
        {
            OperationClaim claim = new() { Name = $"restaurantid{restaurantId}" };
            OperationClaim addedOperationClaim = await _operationClaimRepository.CreateAsync(claim);

            UserOperationClaim userOperationClaim = new() { OperationClaimId = addedOperationClaim.Id, UserId = ownerId };
            UserOperationClaim addedUserOperationClaim = await _userOperationClaimRepository.CreateAsync(userOperationClaim);
            return;

        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal claims = handler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                return claims;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task RevokeRefreshToken(RefreshToken storedRefreshToken, string requestIpAdress, string token, string revokedForLogin)
        {
            storedRefreshToken.ReplacedByToken = token;
            storedRefreshToken.Revoked = DateTime.Now;
            storedRefreshToken.RevokedByIp = requestIpAdress;
            storedRefreshToken.RevokedReason = revokedForLogin;
            await _refreshTokenRepository.UpdateAsync(storedRefreshToken);
            return;
        }

        public async Task<EmailVertificator> CreateEmailVerificator(User user)
        {
            string activationCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();
            EmailVertificator emailVertificator = new() { ActivationCode = activationCode, UserId = user.Id, IsVerified = false};
            return emailVertificator;
        }

        public async Task SecondFactorSecurity(User user, string AuthenticationCode)
        {
            if (user.VerificationType == VerificationType.None) return;

            
            if (user.VerificationType == VerificationType.Email)
            {
                EmailVertificator emailVerificator =
                    await _emailVertificatorRepository.GetAsync(x => x.UserId == user.Id);
                _authBusinessRules.DoesEmailVerificatorExists(emailVerificator);
                if (IsAuthenticationCodeNull(AuthenticationCode))
                {
                    string code = await _emailAuthenticatorHelper.CreateEmailActivationKey();
                    emailVerificator.ActivationCode = code;
                    await _emailVertificatorRepository.UpdateAsync(emailVerificator);
                    SendActivationCode(user, code);
                    throw new BusinessException("The authentication code has been sent to your email.");

                }

                if (!await EmailVerificationFactor(emailVerificator, AuthenticationCode))
                    throw new BusinessException("Authentication code is not correct.");
                return;
            }

            if (user.VerificationType == VerificationType.Otp)
            {
                if (!await OtpVerificationFactor(user, AuthenticationCode))
                    throw new BusinessException("Authentication code is not correct.");
                return;
            }

            throw new BusinessException("Second factor authentication is not successful.");

        }

        public async Task DoesOtpCodeMatches(string otpVerificatorSecretKey, string code)
        {
            byte[] secretKeyBytes = Base32Encoding.ToBytes(otpVerificatorSecretKey);
            bool result = await _otpVertificationHelper.VerifyCode(secretKeyBytes, code);
            if (!result) throw new BusinessException("Otp code does not match.");
        }


        public RefreshToken CreateRefreshToken(Owner owner, string ipAddress)
        {
            return _tokenHelper.CreateRefreshToken(owner, ipAddress);
        }

        public bool IsTokenValid(string accessToken)
        {
            JwtSecurityTokenHandler jwtHandler = new();
            jwtHandler.ValidateToken(accessToken, _tokenValidationParameters, out var validatedToken);
            return true;

        }


        #region private methods

        private async Task SendActivationCode(User user, string code)
        {


            Mail mail = new Mail()
            {
                Subject = "Restaurant Menu Authentication Code",
                HtmlBody = $"Your authenticaton code is :{code}",
                ToEmail = user.Email,
                ToFullName = $"{user.FirstName} {user.LastName}"
            };
            await _mailService.SendEmail(mail);
        }
        private bool IsAuthenticationCodeNull(string authenticationCode)
        {
            return authenticationCode == null || authenticationCode == "" || authenticationCode == "string";
        }

        private async Task<bool> OtpVerificationFactor(User user, string authenticationCode)
        {
            OtpVerificator otpVerificator = await _otpVerificatorRepository.GetAsync(x => x.UserId == user.Id);
            _authBusinessRules.DoesOtpVerificatorExists(otpVerificator);
            bool result = await _otpVertificationHelper.VerifyCode(Base32Encoding.ToBytes(otpVerificator.SecretKey), authenticationCode);
            return result;
        }

        private async Task<bool> EmailVerificationFactor(EmailVertificator emailVertificator, string authenticationCode)
        {
            
            
            return authenticationCode == emailVertificator.ActivationCode;
            
        }

        private async Task<List<OperationClaim>> GetUserClaims(Owner owner)
        {
            // gets 10 claims maybe can cause errors in future when claims get more
            return (await _userOperationClaimRepository.GetListAsync(x => x.UserId == owner.Id, include: x => x.Include(x => x.OperationClaim))).Items
                                           .Select(x => new OperationClaim() { Id = x.OperationClaim.Id, CreatedDate = x.OperationClaim.CreatedDate, Name = x.OperationClaim.Name }).ToList();

        }

        private async Task<List<Claim>>? CreateRestaurantIdClaims(Owner owner)
        {
            if (owner.Restaurants.Count == 0) return null;

            List<Claim> claims = new List<Claim>();
            owner.Restaurants.ToList().ForEach(r => claims.Add(new Claim("RestaurantId", r.Id.ToString())));
            return claims;
            
        }

        
        #endregion

    }
}
