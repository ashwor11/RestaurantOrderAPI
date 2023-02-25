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

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommand : IRequest<AuthResponseDto>
    {
        public UserForRegisterDto UserForRegisterDto{ get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
        {
            private readonly IAuthService _authService;
            private readonly IAuthRepository _authRepository;
            private readonly AuthBusinessRules _businessRules;

            public RegisterCommandHandler(IAuthService authService, IAuthRepository authRepository, AuthBusinessRules businessRules)
            {
                _authService = authService;
                _authRepository = authRepository;
                _businessRules = businessRules;
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
                    VertificationType = 0
                };

                Owner createdOwner = await _authRepository.CreateAsync(newOwner);

                AccessToken accessToken = await _authService.CreateAccessToken(createdOwner);

                AuthResponseDto response = new()
                {
                    AccessToken = accessToken
                };


                return response;
            }
        }
    }
}
