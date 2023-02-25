using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Application.Pipelines.Validation;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Core.Security.Mailing;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<AuthResponseDto>
    {
        public UserForRegisterDto UserForRegisterDto{ get; set; }
        public string IpAddress { get; set; }

        public string EmailVerificationPrefix { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
        {
            private readonly IAuthService _authService;
            private readonly IAuthRepository _authRepository;
            private readonly AuthBusinessRules _businessRules;
            private readonly IRefreshTokenRepository _refreshTokenRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly IMailService _mailService;

            public RegisterCommandHandler(IAuthService authService, IAuthRepository authRepository, AuthBusinessRules businessRules, IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper, IMailService mailService)
            {
                _authService = authService;
                _authRepository = authRepository;
                _businessRules = businessRules;
                _refreshTokenRepository = refreshTokenRepository;
                _tokenHelper = tokenHelper;
                _mailService = mailService;
            }

            public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.IsUserAlreadyExists(request.UserForRegisterDto.Email);

                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out passwordHash, out passwordSalt);
                Owner newOwner = new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    IsVerified = false,
                    VerificationType = 0
                };

                Owner createdOwner = await _authRepository.CreateAsync(newOwner);

                AccessToken accessToken = await _authService.CreateAccessToken(createdOwner);
                RefreshToken refreshToken = _authService.CreateRefreshToken(createdOwner, request.IpAddress);
                await _refreshTokenRepository.CreateAsync(refreshToken);

                string activationToken = _tokenHelper.CreateEmailVerificationToken(createdOwner);
                Mail mail = new()
                {
                    HtmlBody = $"{request.EmailVerificationPrefix}?ActivationToken={HttpUtility.UrlEncode(activationToken)}",
                    ToFullName = $"{createdOwner.FirstName} {createdOwner.LastName}", Subject = "Verify your account.",
                    ToEmail = createdOwner.Email, TextBody = "Please verify your Restaurant Menu account"
                };

                await _mailService.SendEmail(mail);


                AuthResponseDto response = new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token
                };


                return response;
            }
        }
    }
}
