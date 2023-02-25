using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Vertification.OtpVertification;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.VerifyOtpVerification;

public class VerifyOtpVerificationCommand : IRequest
{
    public int UserId { get; set; }
    public string Code { get; set; }

    public class VerifyOtpVerificationCommandHandler : IRequestHandler<VerifyOtpVerificationCommand>
    {
        private readonly IOtpVerificatorRepository _otpVerificatorRepository;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IAuthRepository _authRepository;
        private readonly IAuthService _authService;

        public VerifyOtpVerificationCommandHandler(IOtpVerificatorRepository otpVerificatorRepository, AuthBusinessRules authBusinessRules, IAuthRepository authRepository, IAuthService authService)
        {
            _otpVerificatorRepository = otpVerificatorRepository;
            _authBusinessRules = authBusinessRules;
            _authRepository = authRepository;
            _authService = authService;
        }

        public async Task Handle(VerifyOtpVerificationCommand request, CancellationToken cancellationToken)
        {
            OtpVerificator otpVerificator = await _otpVerificatorRepository.GetAsync(x => x.UserId == request.UserId);
            _authBusinessRules.DoesOtpVerificatorExists(otpVerificator);

            Owner? owner= await _authRepository.GetAsync(x => x.Id == request.UserId);
            _authBusinessRules.IsUserExists(owner);


            await _authService.DoesOtpCodeMatches(otpVerificator.SecretKey, request.Code);

            owner.VerificationType = VerificationType.Otp;
            otpVerificator.IsVerified = true;

            await _authRepository.UpdateAsync(owner);
            await _authRepository.UpdateAsync(owner);

            return;

        }
    }
}