using Application.Features.Restaurants.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Models
{
    public class GetAllRestaurantsModel : BasePageableModel
    {
        public ICollection<GetRestaurantDto> Restaurants { get; set; }

    }
}
