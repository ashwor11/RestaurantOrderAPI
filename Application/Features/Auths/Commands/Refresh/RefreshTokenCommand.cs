using System.Security.Claims;
using Application.Features.Auths.Dtos;
using Application.Features.Auths.Messages;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.Refresh;

public class RefreshTokenCommand : IRequest<AuthResponseDto>
{
    public RefreshTokenDto RefreshTokenDto { get; set; }
    public string IpAdress { get; set; }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand,AuthResponseDto>
    {
        public RefreshTokenCommandHandler(IAuthRepository authRepository, AuthBusinessRules authBusinessRules, IAuthService authService, IRefreshTokenRepository refreshTokenRepository)
        {
            _authRepository = authRepository;
            _authBusinessRules = authBusinessRules;
            _authService = authService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        private readonly IAuthRepository _authRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthService _authService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public async Task<AuthResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _authBusinessRules.IsTokenValid(request.RefreshTokenDto.AccessToken);

            ClaimsPrincipal claims = _authService.GetPrincipalFromToken(request.RefreshTokenDto.AccessToken);

            RefreshToken ?storedRefreshToken = await 
                _refreshTokenRepository.GetAsync(x => x.Token == request.RefreshTokenDto.RefreshToken);
            _authBusinessRules.RefreshTokenMustBeExistWhenRequested(storedRefreshToken);
            _authBusinessRules.RefreshTokenMustNotBeExpired(storedRefreshToken);
            _authBusinessRules.RefreshTokenAndAccessTokenMustBeOwnedBySameUser(storedRefreshToken, claims);

            Owner? owner = await _authRepository.GetAsync(x => x.Id == storedRefreshToken.UserId);
            _authBusinessRules.IsUserExists(owner);

            AccessToken newAccessToken = await _authService.CreateAccessToken(owner);
            RefreshToken newRefreshToken = _authService.CreateRefreshToken(owner, request.IpAdress);

            _refreshTokenRepository.CreateAsync(newRefreshToken);

            await _authService.RevokeRefreshToken(storedRefreshToken, request.IpAdress, newRefreshToken.Token,
                AuthMessages.RevokedForLogin);

            AuthResponseDto response = new()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };

            return response;



        }
    }
}