using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Dtos
{
    public class DrinkForListingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Explanation { get; set; }
        public double? Calories { get; set; }
        public bool HasStock { get; set; } = true;
        public int ?Volume { get; set; }
    }
}
