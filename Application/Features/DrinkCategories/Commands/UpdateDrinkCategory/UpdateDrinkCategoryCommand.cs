using Application.Features.DrinkCategories.Dtos;
using Application.Features.DrinkCategories.Rules;
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

namespace Application.Features.DrinkCategories.Commands.UpdateDrinkCategory
{
    public class UpdateDrinkCategoryCommand : IRequest<UpdatedDrinkCategoryDto>
    {
        public UpdateDrinkCategoryDto UpdateDrinkCategoryDto { get; set; }
        public int OwnerId { get; set; }

        public class UpdateDrinkCategoryCommandHandler : IRequestHandler<UpdateDrinkCategoryCommand, UpdatedDrinkCategoryDto>
        {
            private readonly IDrinkCategoryRepository _drinkCategoryRepository;
            private readonly GeneralBusinessRules _businessRules;
            private readonly DrinkCategoryBusinessRules _drinkCategoryBusinessRules;
            private readonly IMapper _mapper;
            public async Task<UpdatedDrinkCategoryDto> Handle(UpdateDrinkCategoryCommand request, CancellationToken cancellationToken)
            {
                DrinkCategory ?drinkCategory = await _drinkCategoryRepository.GetAsync(x=> x.Id == request.UpdateDrinkCategoryDto.Id, x=> x.Include(x=>x.Menu));
                _drinkCategoryBusinessRules.DoesDrinkCategoryExists(drinkCategory);
                await _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, drinkCategory.Menu.RestaurantId);

                drinkCategory.Name = request.UpdateDrinkCategoryDto.Name;
                DrinkCategory updatedDrinkCategory = await _drinkCategoryRepository.UpdateAsync(drinkCategory);
                UpdatedDrinkCategoryDto updatedDrinkCategoryDto = _mapper.Map<UpdatedDrinkCategoryDto>(updatedDrinkCategory);
                return updatedDrinkCategoryDto;

            }
        }
    }
}
