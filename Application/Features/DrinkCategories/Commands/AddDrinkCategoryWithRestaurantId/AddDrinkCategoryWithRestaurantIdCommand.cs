using Application.Features.DrinkCategories.Dtos;
using Application.Services.Repositories;
using Application.Services.RestaurantService;
using Application.Services.Rules;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DrinkCategories.Commands.AddDrinkCategoryWithRestaurantId
{
    public class AddDrinkCategoryWithRestaurantIdCommand : IRequest<CreatedDrinkCategoryDto>
    {
        public CreateDrinkCategoryWithRestaurantIdDto CreateDrinkCategoryWithRestaurantIdDto { get; set; }
        public int OwnerId { get; set; }


        public class AddDrinkCategoryWithRestaurantIdCommandHandler : IRequestHandler<AddDrinkCategoryWithRestaurantIdCommand, CreatedDrinkCategoryDto>
        {
            private readonly IDrinkCategoryRepository _drinkCategoryRepository;
            private readonly IRestaurantService _restaurantService;
            private readonly GeneralBusinessRules _businessRules;

            public AddDrinkCategoryWithRestaurantIdCommandHandler(IDrinkCategoryRepository drinkCategoryRepository, IRestaurantService restaurantService, GeneralBusinessRules businessRules)
            {
                _drinkCategoryRepository = drinkCategoryRepository;
                _restaurantService = restaurantService;
                _businessRules = businessRules;
            }

            public async Task<CreatedDrinkCategoryDto> Handle(AddDrinkCategoryWithRestaurantIdCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId,request.CreateDrinkCategoryWithRestaurantIdDto.RestaurantId);
                int menuId = await _restaurantService.GetMenuIdWithRestaurantId(request.CreateDrinkCategoryWithRestaurantIdDto.RestaurantId);
                

                DrinkCategory drinkCategory = new() { MenuId = menuId, Name = request.CreateDrinkCategoryWithRestaurantIdDto.Name };

                DrinkCategory foodCreatedCategory = await _drinkCategoryRepository.CreateAsync(drinkCategory);

                return new CreatedDrinkCategoryDto() { Id = foodCreatedCategory.Id, Name = foodCreatedCategory.Name, MenuId = menuId, RestaurantId = request.CreateDrinkCategoryWithRestaurantIdDto.RestaurantId };

            }
        }
    }
}
