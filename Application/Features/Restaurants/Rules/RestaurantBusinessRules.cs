﻿using Application.Services.AuthService;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Rules
{
    public class RestaurantBusinessRules
    {
        private readonly IAuthService _authService;

        public RestaurantBusinessRules(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task DoesOwnerExistAndHaveRestaurantWithSameName(int ownerId, string restaurantName) {
            Owner owner = await _authService.GetOwnerById(ownerId);
            if (owner == null) throw new BusinessException("There is no owner with specified owner id.");
            if(owner.Restaurants.Any(x => x.Name == restaurantName)) throw new BusinessException($"Owner already has a restaurant named {restaurantName}.");
            return;

        }

        public void DoesRestaurantExist(Restaurant restaurant)
        {
            if (restaurant == null) throw new BusinessException("No existing restaurant with this id.");
        }

    }
}
