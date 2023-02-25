using System.Security.Cryptography.X509Certificates;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.VerifyEmailVertification;

public class VerifyEmailVerificationCommand : IRequest
{
    public string ActivationKey { get; set; }


    public class VeriyfyEmailVerificationCommandHandler : IRequestHandler<VerifyEmailVerificationCommand>
    {
        private readonly IEmailVertificatorRepository _emailVertificatorRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthRepository _authRepository;

        public VeriyfyEmailVerificationCommandHandler(IEmailVertificatorRepository emailVertificatorRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IAuthRepository authRepository)
        {
            _emailVertificatorRepository = emailVertificatorRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _authRepository = authRepository;
        }


        public async Task Handle(VerifyEmailVerificationCommand request, CancellationToken cancellationToken)
        {
            EmailVertificator emailVertificator = await 
                _emailVertificatorRepository.GetAsync(x => x.ActivationCode == request.ActivationKey, cancellationToken: cancellationToken);


            _authBusinessRules.DoesEmailVerificatorExists(emailVertificator);
            Owner owner = await _authService.GetOwnerById(emailVertificator.UserId);
            _authBusinessRules.IsUserExists(owner);

            emailVertificator.IsVerified = true;
            emailVertificator.ActivationCode = null;
            owner.VerificationType = VerificationType.Email;

            await _emailVertificatorRepository.UpdateAsync(emailVertificator);

            await _authRepository.UpdateAsync(owner);

            return;





        }
    }
}