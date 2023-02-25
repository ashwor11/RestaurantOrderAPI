using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Vertification.OtpVertification;
using Core.Utility.QrCode;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.EnableOtpVerification;

public class EnableOtpVerificationCommand : IRequest<EnableOtpVerificationDto>
{
    public int UserId { get; set; }

    public class EnableOtpVerificationCommandHandler : IRequestHandler<EnableOtpVerificationCommand, EnableOtpVerificationDto>
    {
        private readonly IOtpVerificatorRepository _otpVerificatorRepository;
        private readonly IOtpVertificationHelper _otpVertificationHelper;
        private readonly IAuthRepository _authRepository;
        private readonly IQrCodeHelper _qrCodeHelper;
        

        public EnableOtpVerificationCommandHandler(IOtpVerificatorRepository otpVerificatorRepository, IOtpVertificationHelper otpVertificationHelper, IAuthRepository authRepository, IQrCodeHelper qrCodeHelper)
        {
            _otpVerificatorRepository = otpVerificatorRepository;
            _otpVertificationHelper = otpVertificationHelper;
            _authRepository = authRepository;
            _qrCodeHelper = qrCodeHelper;
        }

        public async Task<EnableOtpVerificationDto> Handle(EnableOtpVerificationCommand request, CancellationToken cancellationToken)
        {
            byte[] secretKeyBytes = await _otpVertificationHelper.GenerateSecretKey();
            string secretKey = await _otpVertificationHelper.ConvertSecretKeyToString(secretKeyBytes);

            Owner owner = await _authRepository.GetAsync(x => x.Id == request.UserId);
            OtpVerificator? dbOtpVerificator = await _otpVerificatorRepository.GetAsync(x => x.UserId == request.UserId);

            if (dbOtpVerificator != null)
            {
                dbOtpVerificator.SecretKey = secretKey;
                await _otpVerificatorRepository.UpdateAsync(dbOtpVerificator);
                
                
                owner.VerificationType = VerificationType.Otp;
                await _authRepository.UpdateAsync(owner);
            }
            else
            {
                dbOtpVerificator = new() { SecretKey = secretKey, UserId = request.UserId };
                await _otpVerificatorRepository.CreateAsync(dbOtpVerificator);
            }

            string uri = _otpVertificationHelper.CreateOtpUri(secretKey, owner.Email, "Restaurant Menu");
            string qrCode = _qrCodeHelper.CreateTotpQrCode(uri);
            EnableOtpVerificationDto enableOtpVerificationDto = new() { SecretKey = secretKey , QrCode = qrCode};

            return enableOtpVerificationDto;

        }
    }
}