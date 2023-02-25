using System.Web;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Mailing;
using Core.Security.Vertification.EmailVertification;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auths.Commands.EnableEmailVertification;

public class EnableEmailVertificationCommand : IRequest
{
    public int UserId { get; set; }
    public string EnableEmailPrefix { get; set; }

    public class EnableEmailVertificationCommandHandler : IRequestHandler<EnableEmailVertificationCommand>
    {
        public EnableEmailVertificationCommandHandler(IAuthRepository authRepository, IAuthService authService, AuthBusinessRules authBusinessRules, IEmailVertificatorRepository emailVertificatorRepository, IMailService mailService)
        {
            _authRepository = authRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _emailVertificatorRepository = emailVertificatorRepository;
            _mailService = mailService;
        }

        private readonly IAuthRepository _authRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailVertificatorRepository _emailVertificatorRepository;
        private readonly IMailService _mailService;

        
        public async Task Handle(EnableEmailVertificationCommand request, CancellationToken cancellationToken)
        {
            Owner? owner = await _authRepository.GetAsync(x => x.Id == request.UserId);
            _authBusinessRules.IsUserExists(owner);


            EmailVertificator dbEmailVertificator = await _emailVertificatorRepository.GetAsync(x => x.Id == request.UserId);

            if (dbEmailVertificator != null)
            {
                owner.VerificationType = VerificationType.Email;
                await _authRepository.UpdateAsync(owner);
                return;
            }

            EmailVertificator emailVertificator = await _authService.CreateEmailVerificator(owner);
            EmailVertificator addedEmailVertificator =
                await _emailVertificatorRepository.CreateAsync(emailVertificator);

            Mail mail = new()
            {
                HtmlBody =
                    $"{request.EnableEmailPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailVertificator.ActivationCode)}",
                Subject = "Verify your email.",
                ToEmail = owner.Email,
                ToFullName = $"{owner.FirstName} {owner.LastName}",

            };

            await _mailService.SendEmail(mail);

            return;


        }
    }
}