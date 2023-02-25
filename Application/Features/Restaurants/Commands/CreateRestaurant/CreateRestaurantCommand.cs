using Application.Features.Restaurants.Dtos;
using Application.Features.Restaurants.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Pipelines.Logging;

namespace Application.Features.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommand : IRequest<CreatedRestaurantResponseDto>, ILoggableRequest
    {
        public int OwnerId { get; set; }
        public string RestaurantName { get; set; }

        public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, CreatedRestaurantResponseDto>
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly RestaurantBusinessRules _restaurantBusinessRules;
            private readonly IAuthService _authService;

            public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, RestaurantBusinessRules restaurantBusinessRules, IAuthService authService)
            {
                _restaurantRepository = restaurantRepository;
                _restaurantBusinessRules = restaurantBusinessRules;
                _authService = authService;
            }

            public async Task<CreatedRestaurantResponseDto> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
            {
                await _restaurantBusinessRules.DoesOwnerExistAndHaveRestaurantWithSameName(request.OwnerId, request.RestaurantName);

                Menu menu = new();

                Restaurant restaurant = new()
                {
                    Name = request.RestaurantName,
                    OwnerId = request.OwnerId,
                    Menu = menu,
                };
                menu.Resturant = restaurant;
                restaurant.Menu = menu;
                Restaurant createdRestaurant = await _restaurantRepository.CreateAsync(restaurant);

                CreatedRestaurantResponseDto response = new()
                {
                    Id = createdRestaurant.Id,
                    Name = createdRestaurant.Name,
                    OwnerId = createdRestaurant.OwnerId
                };
                await _authService.AddRestaurantClaim(request.OwnerId, response.Id);
                return response;

            }

            

            
        }
    }
}
