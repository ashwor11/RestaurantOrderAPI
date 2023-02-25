using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DrinkCategories.Commands.UpdateDrinkCategory
{
    public class UpdateDrinkCategoryCommandValidator : AbstractValidator<UpdateDrinkCategoryCommand>
    {
        public UpdateDrinkCategoryCommandValidator()
        {
            RuleFor(x=> x.UpdateDrinkCategoryDto).NotEmpty();
        }
    }
}
