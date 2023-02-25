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
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Foods.Commands.DeleteFood
{
    public class DeleteFoodCommand : IRequest<CommandResponseFoodDto>
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public class DeleteFoodCommandHandler : IRequestHandler<DeleteFoodCommand, CommandResponseFoodDto>
        {
            private readonly IFoodRepository _foodRepository;
            private readonly GeneralBusinessRules _generealBusinessRules;
            private readonly FoodBusinessRules _foodBusinessRules;
            private readonly IMapper _mapper;

            public DeleteFoodCommandHandler(IFoodRepository foodRepository, GeneralBusinessRules generealBusinessRules, FoodBusinessRules foodBusinessRules, IMapper mapper)
            {
                _foodRepository = foodRepository;
                _generealBusinessRules = generealBusinessRules;
                _foodBusinessRules = foodBusinessRules;
                _mapper = mapper;
            }

            public async Task<CommandResponseFoodDto> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
            {
                Food? food = await _foodRepository.GetAsync(x => x.Id == request.Id, x => x.Include(x => x.FoodCategory).ThenInclude(x => x.Menu));
                _foodBusinessRules.DoesFoodExists(food);
                await _generealBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, food.FoodCategory.Menu.RestaurantId);

                Food? deletedFood = await _foodRepository.DeleteAsync(food);
                CommandResponseFoodDto response = _mapper.Map<CommandResponseFoodDto>(deletedFood);
                return response;

            }
        }
    }
}
