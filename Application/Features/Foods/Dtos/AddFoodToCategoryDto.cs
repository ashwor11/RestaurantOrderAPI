using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Dtos
{
    public class AddFoodToCategoryDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int FoodCategoryId { get; set; }
        public bool? HasStock { get; set; } = true;
        public string? Explanation { get; set; }
        public int Calories { get; set; }


    }
}
