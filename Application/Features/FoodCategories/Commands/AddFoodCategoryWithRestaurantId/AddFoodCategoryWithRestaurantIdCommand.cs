using Application.Features.Categories.Dtos;
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

namespace Application.Features.Categories.Commands.AddCategoryWithRestaurantId
{
    public class AddFoodCategoryWithRestaurantIdCommand : IRequest<CreatedFoodCategoryDto>
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }

        public class AddFoodCategoryWithRestaurantIdCommandHandler : IRequestHandler<AddFoodCategoryWithRestaurantIdCommand, CreatedFoodCategoryDto>
        {
            private readonly IFoodCategoryRepository _categoryRepository;
            private readonly IRestaurantService _restaurantService;
            private readonly GeneralBusinessRules _businessRules;

            public AddFoodCategoryWithRestaurantIdCommandHandler(IFoodCategoryRepository categoryRepository, IRestaurantService restaurantService, GeneralBusinessRules businessRules)
            {
                _categoryRepository = categoryRepository;
                _restaurantService = restaurantService;
                _businessRules = businessRules;
            }

            public async Task<CreatedFoodCategoryDto> Handle(AddFoodCategoryWithRestaurantIdCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, request.RestaurantId);
                int menuId = await _restaurantService.GetMenuIdWithRestaurantId(request.RestaurantId);

                FoodCategory foodCategory = new() { MenuId = menuId, Name = request.Name };

                FoodCategory foodCreatedCategory = await _categoryRepository.CreateAsync(foodCategory);

                return new CreatedFoodCategoryDto() { Id = foodCreatedCategory.Id, Name = foodCreatedCategory.Name, MenuId = menuId, RestaurantId = request.RestaurantId };

                
            }
        }
    }
}
