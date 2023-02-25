using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DrinkCategories.Dtos
{
    public class CreatedDrinkCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MenuId { get; set; }
        public int RestaurantId { get; set; }
    }
}
