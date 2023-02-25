using System.Security.AccessControl;
using Application.Features.Categories.Dtos;
using Application.Features.FoodCategories.Rules;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FoodCategories.Commands.DeleteFoodCategory;

public class DeleteFoodCategoryCommand : IRequest<DeletedFoodCategoryDto>
{
    public int Id { get; set; }
    public int OwnerId { get; set; }

    public class DeleteFoodCategoryCommandHandler : IRequestHandler<DeleteFoodCategoryCommand, DeletedFoodCategoryDto>
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        private readonly GeneralBusinessRules _businessRules;
        private readonly FoodCategoryBusinessRules _foodCategoryBusinessRules;
        private readonly IMapper _mapper;
        public async Task<DeletedFoodCategoryDto> Handle(DeleteFoodCategoryCommand request, CancellationToken cancellationToken)
        {
            FoodCategory? foodCategory = await _foodCategoryRepository.GetAsync(x => x.Id == request.Id,
                x => x.Include((x => x.Foods)).Include(x => x.Menu));
            _foodCategoryBusinessRules.DoesFoodCategoryExist(foodCategory);
            _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, foodCategory.Menu.RestaurantId);

            await _foodCategoryRepository.DeleteAsync(foodCategory);

            DeletedFoodCategoryDto response = _mapper.Map<DeletedFoodCategoryDto>(foodCategory);
            return response;
        }
    }
}