using Application.Features.Foods.Dtos;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Commands.AddFoodToCategory
{
    public class AddFoodToCategoryCommand : IRequest<CommandResponseFoodDto>
    {
        public AddFoodToCategoryDto AddFoodToCategoryDto { get; set; }
        public int OwnerId { get; set; }

        public class AddFoodToCategoryCommandHandler : IRequestHandler<AddFoodToCategoryCommand, CommandResponseFoodDto>
        {
            private readonly IFoodRepository _foodRepository;
            private readonly IFoodCategoryRepository _foodCategoryRepository;
            private readonly GeneralBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public AddFoodToCategoryCommandHandler(IFoodRepository foodRepository, IFoodCategoryRepository foodCategoryRepository, GeneralBusinessRules businessRules, IMapper mapper)
            {
                _foodRepository = foodRepository;
                _foodCategoryRepository = foodCategoryRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<CommandResponseFoodDto> Handle(AddFoodToCategoryCommand request, CancellationToken cancellationToken)
            {

                FoodCategory? foodCategory = await _foodCategoryRepository.GetAsync(x => x.Id == request.AddFoodToCategoryDto.FoodCategoryId, x => x.Include(x => x.Menu.Resturant));
                await _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, foodCategory.Menu.Resturant.Id);
                Food food= _mapper.Map<Food>(request.AddFoodToCategoryDto);
                food.RestaurantId = foodCategory.Menu.RestaurantId;

                Food addedFood= await _foodRepository.CreateAsync(food);

                CommandResponseFoodDto response = _mapper.Map<CommandResponseFoodDto>(addedFood);
                return response;

            }
        }
    }
}
