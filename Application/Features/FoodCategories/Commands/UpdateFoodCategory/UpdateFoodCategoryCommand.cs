using Application.Features.Categories.Dtos;
using Application.Features.DrinkCategories.Commands.UpdateDrinkCategory;
using Application.Features.DrinkCategories.Dtos;
using Application.Features.DrinkCategories.Rules;
using Application.Features.FoodCategories.Rules;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FoodCategories.Commands.UpdateFoodCategory;

public class UpdateFoodCategoryCommand : IRequest<UpdatedFoodCategoryDto>
{
    public UpdateFoodCategoryDto UpdateFoodCategoryDto { get; set; }
    public int OwnerId { get; set; }

    public class UpdateDrinkCategoryCommandHandler : IRequestHandler<UpdateFoodCategoryCommand, UpdatedFoodCategoryDto>
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        private readonly GeneralBusinessRules _businessRules;
        private readonly FoodCategoryBusinessRules _foodCategoryBusinessRules;
        private readonly IMapper _mapper;
        public async Task<UpdatedFoodCategoryDto> Handle(UpdateFoodCategoryCommand request, CancellationToken cancellationToken)
        {
            FoodCategory? foodCategory =
                await _foodCategoryRepository.GetAsync(x => x.Id == request.UpdateFoodCategoryDto.Id,x=>x.Include(x=>x.Menu),
                    cancellationToken: cancellationToken);
            _foodCategoryBusinessRules.DoesFoodCategoryExist(foodCategory);
            _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, foodCategory.Menu.RestaurantId);
            foodCategory.Name = request.UpdateFoodCategoryDto.Name;
            FoodCategory? updatedFoodCategory = await _foodCategoryRepository.UpdateAsync(foodCategory);

            return _mapper.Map<UpdatedFoodCategoryDto>(updatedFoodCategory);

        }
    }
}