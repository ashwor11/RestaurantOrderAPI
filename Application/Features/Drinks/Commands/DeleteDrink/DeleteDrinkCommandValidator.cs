using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Commands.DeleteDrink
{
    public class DeleteDrinkCommandValidator : AbstractValidator<DeleteDrinkCommand>
    {
        public DeleteDrinkCommandValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id can not be empty.");
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("You are not authorized.");
        }
    }
}
