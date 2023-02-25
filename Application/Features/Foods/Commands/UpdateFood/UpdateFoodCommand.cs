using Application.Features.Foods.Dtos;
using Application.Features.Foods.Rules;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Commands.UpdateFood
{
    public class UpdateFoodCommand : IRequest<CommandResponseFoodDto>
    {
        public UpdateFoodDto UpdateFoodDto { get; set; }
        public int OwnerId { get; set; }

        public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, CommandResponseFoodDto>
        {
            private readonly IFoodRepository _foodRepository;
            private readonly FoodBusinessRules _foodBusinessRules;
            private readonly GeneralBusinessRules _generalBusinessRules;
            private readonly IMapper _mapper;

            public UpdateFoodCommandHandler(IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules, GeneralBusinessRules generalBusinessRules, IMapper mapper)
            {
                _foodRepository = foodRepository;
                _foodBusinessRules = foodBusinessRules;
                _generalBusinessRules = generalBusinessRules;
                _mapper = mapper;
            }

            public async Task<CommandResponseFoodDto> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
            {
               Food ?food = await _foodRepository.GetAsync(x => x.Id == request.UpdateFoodDto.Id, x=> x.Include(x=>x.FoodCategory).ThenInclude(x=> x.Menu));
                _foodBusinessRules.DoesFoodExists(food);
                _generalBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, food.FoodCategory.Menu.RestaurantId);
                SetProperties(request.UpdateFoodDto, food);
                Food updatedFood = await _foodRepository.UpdateAsync(food);

                CommandResponseFoodDto foodDto = _mapper.Map<CommandResponseFoodDto>(updatedFood);
                return foodDto;

               
            }

            private void SetProperties(UpdateFoodDto updateFoodDto, Food food)
            {
                PropertyInfo[] properties = typeof(UpdateFoodDto).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if(property.GetValue(updateFoodDto) != null) typeof(Food).GetProperty(property.Name).SetValue(food,property.GetValue(updateFoodDto));
                }
                
            }
        }
    }
}
