using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Dtos;

namespace Application.Features.Auths.Commands.Register
{
    public class RegisterCommandValidation : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x=> x.UserForRegisterDto.FirstName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.LastName).NotEmpty();
            RuleFor(x => x.UserForRegisterDto.Email).EmailAddress().NotNull();
            RuleFor(x => x.UserForRegisterDto.Password).MinimumLength(8).MaximumLength(32).NotEmpty().NotNull();
        }
    }
}
