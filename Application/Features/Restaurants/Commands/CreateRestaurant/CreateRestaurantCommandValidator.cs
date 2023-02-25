using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Commands.CreateRestaurant
{
    public  class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(x=>x.RestaurantName).NotEmpty();
            RuleFor(x=>x.OwnerId).NotEmpty();
        }
    }
}
