using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Dtos
{
    public class MenuForListingDto
    {
        public int Id { get; set; }
        public ICollection<FoodCategoryForListingDto> FoodCategories{ get; set; }
        public ICollection<DrinkCategoryForListingDto> DrinkCategories { get; set; }

    }
}
