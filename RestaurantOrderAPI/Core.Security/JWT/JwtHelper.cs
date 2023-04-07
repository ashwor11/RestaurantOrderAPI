﻿using Core.Security.Encryption.Helpers;
using Core.Security.Entities;
using Core.Security.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Core.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly EmailVerificationTokenOptions _emailVerificationTokenOptions;

        public JwtHelper(IConfiguration configuration, IOptions<TokenValidationParameters> tokenValidationParameters, IOptions<TokenOptions> tokenOptions, IOptions<EmailVerificationTokenOptions> emailVerificationTokenOptions)
        {

            _tokenOptions = tokenOptions.Value;
            _tokenValidationParameters = tokenValidationParameters.Value;
            _emailVerificationTokenOptions = emailVerificationTokenOptions.Value;

        }




        public AccessToken CreateToken(User user, IList<OperationClaim> operationClaims)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = CreateJwtSecurityToken(user, _tokenOptions, operationClaims);

            string? token = handler.WriteToken(jwt);
            return new AccessToken() { Token = token, ExpireTime = jwt.ValidTo };

        }

        public RefreshToken CreateRefreshToken(User user, string ipAdress)
        {


            string token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            RefreshToken refreshToken = new()
            {
                UserId = user.Id,
                Token = token,
                Created = DateTime.UtcNow,
                CreatedByIp = ipAdress,
                ExpireDate = DateTime.UtcNow.AddDays(_tokenOptions.RefreshTokenTTL),
            };

            return refreshToken;

        }

        public bool IsTokenExpired(AccessToken accessToken)
        {
            if (DateTime.UtcNow > accessToken.ExpireTime) return true;
            return false;
        }

        public string CreateEmailVerificationToken(User user)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_emailVerificationTokenOptions.SecretKey);
            SigningCredentials credentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            JwtSecurityToken jwt = new(_tokenOptions.Issuer,_tokenOptions.Audience,claims,DateTime.UtcNow,DateTime.UtcNow.AddMinutes(_emailVerificationTokenOptions.TokenExpiration),credentials);
            return handler.WriteToken(jwt);
        }

        private JwtSecurityToken CreateJwtSecurityToken(User user, TokenOptions tokenOptions, IList<OperationClaim> operationClaims)
        {
            SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            SigningCredentials credentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            JwtSecurityToken jwt = new(_tokenOptions.Issuer, _tokenOptions.Audience,
                                                    CreateAndSetClaims(user, operationClaims),
                                                    DateTime.UtcNow, DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration), credentials);
            return jwt;
        }

        private IEnumerable<Claim> CreateAndSetClaims(User user, IList<OperationClaim> operationClaims)
        {
            List<Claim> claims = new List<Claim>();
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddRoles(operationClaims.Select(x => x.Name).ToArray());
            return claims;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            return null;
        }

        public ClaimsPrincipal ValidateEmailVerificationToken(string token, out SecurityToken securityToken)
        {
            JwtSecurityTokenHandler handler = new();
            _tokenValidationParameters.IssuerSigningKey =
                SecurityKeyHelper.CreateSecurityKey(_emailVerificationTokenOptions.SecretKey);
            ClaimsPrincipal claims = handler.ValidateToken(token, _tokenValidationParameters, out securityToken);
            return claims;
        }



    }
}
