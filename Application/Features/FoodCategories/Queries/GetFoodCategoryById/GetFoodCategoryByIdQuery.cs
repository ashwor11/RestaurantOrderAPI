using Application.Features.Categories.Dtos;
using Application.Features.FoodCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FoodCategories.Queries.GetFoodCategoryById;

public class GetFoodCategoryByIdQuery : IRequest<GetFoodCategoryByIdDto>
{
    public int Id { get; set; }
    public class GetFoodCategoryByIdQueryHandler : IRequestHandler<GetFoodCategoryByIdDto,GetFoodCategoryByIdDto>
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        private readonly FoodCategoryBusinessRules _foodCategoryBusinessRules;
        private readonly IMapper _mapper;
        public async Task<GetFoodCategoryByIdDto> Handle(GetFoodCategoryByIdDto request, CancellationToken cancellationToken)
        {
            FoodCategory? foodCategory =
                await _foodCategoryRepository.GetAsync(x => x.Id == request.Id, x => x.Include(x => x.Foods));
            _foodCategoryBusinessRules.DoesFoodCategoryExist(foodCategory);
            GetFoodCategoryByIdDto response = _mapper.Map<GetFoodCategoryByIdDto>(foodCategory);
            return response;
        }
    }
}