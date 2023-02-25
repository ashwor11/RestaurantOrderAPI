using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Exceptions;

namespace Application.Services.RestaurantService
{
    public class RestaurantManager : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantManager(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<int> GetMenuIdWithRestaurantId(int restaurantId)
        {
            Restaurant restaurant = await _restaurantRepository.GetAsync(x => x.Id == restaurantId, include: x => x.Include(x => x.Menu));
            return restaurant.Menu.Id;
        }

        public async Task DoesRestaurantExists(int restaurantId)
        {
            Restaurant restaurant = await _restaurantRepository.GetAsync(x => x.Id == restaurantId);
            if (restaurant == null) throw new BusinessException("Specified restaurant does not exist.");
        }
    }
}
