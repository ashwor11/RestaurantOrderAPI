using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAdress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
        {
            private readonly IAuthService _authService;
            private readonly IAuthRepository _authRepository;
            private readonly AuthBusinessRules _businessRules;
            private readonly ITokenHelper _tokenHelper;
            private readonly IRefreshTokenRepository _refreshTokenRepository;

            public LoginCommandHandler(IAuthService authService, IAuthRepository authRepository, AuthBusinessRules businessRules, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
            {
                _authService = authService;
                _authRepository = authRepository;
                _businessRules = businessRules;
                _tokenHelper = tokenHelper;
                _refreshTokenRepository = refreshTokenRepository;
            }


            public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                Owner? owner = await _authRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email, include: x => x.Include(x => x.Restaurants));
                _businessRules.IsUserExists(owner);
                _authService.ConfirmPasswords(owner, request.UserForLoginDto.Password);

                await _authService.SecondFactorSecurity(owner, request.UserForLoginDto.AuthenticatorCode);

                AccessToken accessToken = await _authService.CreateAccessToken(owner);

                RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(owner, request.IpAdress);

                await _refreshTokenRepository.CreateAsync(refreshToken);


                AuthResponseDto authResponseDto = new()
                {
                    RefreshToken = refreshToken.Token,
                    AccessToken = accessToken,
                };
                return authResponseDto;
                
                

                

            }
        }
    }
}
