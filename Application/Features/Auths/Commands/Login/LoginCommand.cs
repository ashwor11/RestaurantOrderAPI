using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
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

        public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
        {
            private readonly IAuthService _authService;
            private readonly IAuthRepository _authRepository;
            private readonly AuthBusinessRules _businessRules;

            public LoginCommandHandler(IAuthService authService, IAuthRepository authRepository, AuthBusinessRules businessRules)
            {
                _authService = authService;
                _authRepository = authRepository;
                _businessRules = businessRules;
            }

            public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                Owner? owner = await _authRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);
                await _businessRules.IsUserExists(owner);
                _authService.ConfirmPasswords(owner, request.UserForLoginDto.Password);

                AccessToken accessToken = await _authService.CreateAccessToken(owner);

                AuthResponseDto authResponseDto = new()
                {
                    AccessToken = accessToken,
                };
                return authResponseDto;
                
                

                

            }
        }
    }
}
