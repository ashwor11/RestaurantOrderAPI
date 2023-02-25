using Application.Features.Restaurants.Dtos;
using Application.Features.Restaurants.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<CreatedRestaurantResponseDto>
    {
        public int OwnerId { get; set; }
        public string RestaurantName { get; set; }

        public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, CreatedRestaurantResponseDto>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly RestaurantBusinessRules _restaurantBusinessRules;

            public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, RestaurantBusinessRules restaurantBusinessRules)
            {
                _restaurantRepository = restaurantRepository;
                _restaurantBusinessRules = restaurantBusinessRules;
            }

            public async Task<CreatedRestaurantResponseDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
            {
                await _restaurantBusinessRules.DoesOwnerExistAndHaveRestaurantWithSameName(request.OwnerId, request.RestaurantName);

                Restaurant restaurant = new()
                {
                    Menu = new(),
                    Name = request.RestaurantName,
                    OwnerId = request.OwnerId,
                };
                Restaurant createdRestaurant = await _restaurantRepository.CreateAsync(restaurant);

                CreatedRestaurantResponseDto response = new()
                {
                    Id = createdRestaurant.Id,
                    Name = createdRestaurant.Name,
                    OwnerId = createdRestaurant.OwnerId
                };
                return response;

            }

            

            
        }
    }
}
