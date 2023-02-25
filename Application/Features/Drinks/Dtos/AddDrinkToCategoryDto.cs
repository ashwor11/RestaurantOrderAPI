using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Dtos
{
    public class AddDrinkToCategoryDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int DrinkCategoryId { get; set; }
        public bool? HasStock { get; set; } = true;
        public string? Explanation { get; set; }
        public int? Volume { get; set; }
        public int Calories { get; set; }
        

    }
}
