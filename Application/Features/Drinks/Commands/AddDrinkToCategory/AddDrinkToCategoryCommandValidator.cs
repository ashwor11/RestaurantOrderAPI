using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Commands.AddDrinkToCategory
{
    public  class AddDrinkToCategoryCommandValidator: AbstractValidator<AddDrinkToCategoryCommand>
    {
        public AddDrinkToCategoryCommandValidator()
        {
            RuleFor(x=> x.OwnerId).NotEmpty();
            RuleFor(x=> x.AddDrinkToCategoryDto).NotEmpty();
        }
    }
}
