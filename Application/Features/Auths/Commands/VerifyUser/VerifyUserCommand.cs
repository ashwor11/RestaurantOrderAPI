using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Auths.Commands.VerifyUser;

public class VerifyUserCommand : IRequest
{
    public string ActivationToken { get; set; }

    public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand>
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IAuthRepository _authRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly ITokenHelper _tokenHelper;


        public VerifyUserCommandHandler(IOptions<TokenValidationParameters> tokenValidationParameters, IAuthRepository authRepository, AuthBusinessRules authBusinessRules, ITokenHelper tokenHelper)
        {
            _tokenValidationParameters = tokenValidationParameters.Value;
            _authRepository = authRepository;
            _authBusinessRules = authBusinessRules;
            _tokenHelper = tokenHelper;
        }

        public async Task Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            

            SecurityToken token;
            ClaimsPrincipal claims = _tokenHelper.ValidateEmailVerificationToken(request.ActivationToken, out token);

            _authBusinessRules.IsTokenExpired(token);
            _authBusinessRules.ClaimsContainsEmailClaim(claims);
            Owner owner = await _authRepository.GetAsync(x => x.Email == claims.FindFirstValue(ClaimTypes.Email));
            _authBusinessRules.IsUserAlreadyVerified(owner);
            owner.IsVerified = true;
            await _authRepository.UpdateAsync(owner);
            return;
        }
    }
}