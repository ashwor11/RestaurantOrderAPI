using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Rules
{
    public class GeneralBusinessRules
    {
        private readonly IAuthRepository _authRepository;

        public GeneralBusinessRules(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task IsOwnerResponsibleForRestaurant(int ownerId, int restaurantId)
        {
            Owner owner = await _authRepository.GetAsync(x => x.Id == ownerId, x => x.Include(x=> x.Restaurants));
            if (!owner.Restaurants.Any(x => x.Id == restaurantId)) throw new BusinessException("You have no authorization on this restaurant.");
        }
    }
}
