using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.RestaurantService
{
    public interface IRestaurantService
    {
        public Task<int> GetMenuIdWithRestaurantId(int restaurantId);
        public Task DoesRestaurantExists(int restaurantId);
    }
}
