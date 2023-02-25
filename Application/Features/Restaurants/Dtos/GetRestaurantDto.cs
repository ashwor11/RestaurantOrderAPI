using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Dtos
{
    public class GetRestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
