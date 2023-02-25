using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.DrinkCategories.Dtos;
using Application.Features.DrinkCategories.Rules;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DrinkCategories.Commands.DeleteDrinkCategory
{
    public class DeleteDrinkCategoryCommand : IRequest<DeletedDrinkCategoryDto>
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public class DeleteDrinkCategoryCommandHandler : IRequestHandler<DeleteDrinkCategoryCommand,DeletedDrinkCategoryDto>
        {
            public DeleteDrinkCategoryCommandHandler(IDrinkCategoryRepository drinkCategoryRepository, GeneralBusinessRules generalBusinessRules, DrinkCategoryBusinessRules drinkCategoryBusinessRules, IMapper mapper)
            {
                _drinkCategoryRepository = drinkCategoryRepository;
                _generalBusinessRules = generalBusinessRules;
                _drinkCategoryBusinessRules = drinkCategoryBusinessRules;
                _mapper = mapper;
            }

            private readonly IDrinkCategoryRepository _drinkCategoryRepository;
            private readonly GeneralBusinessRules _generalBusinessRules;
            private readonly DrinkCategoryBusinessRules _drinkCategoryBusinessRules;
            private readonly IMapper _mapper;

            public async Task<DeletedDrinkCategoryDto> Handle(DeleteDrinkCategoryCommand request, CancellationToken cancellationToken)
            {
                DrinkCategory ?drinkCategory = await _drinkCategoryRepository.GetAsync(x => x.Id == request.Id, x => x.Include(x => x.Drinks).Include(x=>x.Menu),cancellationToken:cancellationToken);
                _drinkCategoryBusinessRules.DoesDrinkCategoryExists(drinkCategory);
                await _generalBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, drinkCategory.Menu.RestaurantId);
                await _drinkCategoryRepository.DeleteAsync(drinkCategory);
                DeletedDrinkCategoryDto response = _mapper.Map<DeletedDrinkCategoryDto>(drinkCategory);
                return response;

            }
        }
    }
}
