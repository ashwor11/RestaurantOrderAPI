using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Dtos
{
    public class CreateCategoryWithRestaurantIdDto
    {
        public string Name { get; set; }
        public int RestaurantId { get; set; }
    }
}
