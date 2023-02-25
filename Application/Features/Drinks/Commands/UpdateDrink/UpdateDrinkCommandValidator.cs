using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Commands.UpdateDrink
{
    public class UpdateDrinkCommandValidator : AbstractValidator<UpdateDrinkCommand>
    {
        public UpdateDrinkCommandValidator()
        {
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("You are not authorized.");
            RuleFor(x => x.UpdateDrinkDto.Id).NotEmpty().NotNull();
        }
    }
}
