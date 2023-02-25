using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Dtos
{
    public class UpdateFoodDto 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public int? Calories { get; set; }
        public bool? HasStock { get; set; } = true;
        public string? Explanation { get; set; }

    }
}
