using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DrinkCategories.Commands.AddDrinkCategoryWithRestaurantId
{
    public class AddDrinkCategoryWithRestaurantIdCommandValidator : AbstractValidator<AddDrinkCategoryWithRestaurantIdCommand>
    {
        public AddDrinkCategoryWithRestaurantIdCommandValidator()
        {
            RuleFor(x => x.OwnerId).NotEmpty().WithMessage("You are not authorized.");
            RuleFor(x => x.CreateDrinkCategoryWithRestaurantIdDto).NotEmpty();
        }
    }
}
