using Application.Features.Drinks.Dtos;
using Application.Services.Repositories;
using Application.Services.RestaurantService;
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

namespace Application.Features.Drinks.Commands.AddDrinkToCategory
{
    public class AddDrinkToCategoryCommand : IRequest<CommandResponseDrinkDto>
    {
        public AddDrinkToCategoryDto AddDrinkToCategoryDto { get; set; }
        public int OwnerId { get; set; }

        public class AddDrinkToCategoryCommaandHandler : IRequestHandler<AddDrinkToCategoryCommand, CommandResponseDrinkDto>
        {
            private readonly IDrinkRepository _drinkRepository;
            private readonly IDrinkCategoryRepository _drinkCategoryRepository;
            private readonly GeneralBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public AddDrinkToCategoryCommaandHandler(IDrinkRepository drinkRepository, IDrinkCategoryRepository drinkCategoryRepository, GeneralBusinessRules businessRules, IMapper mapper)
            {
                _drinkRepository = drinkRepository;
                _drinkCategoryRepository = drinkCategoryRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<CommandResponseDrinkDto> Handle(AddDrinkToCategoryCommand request, CancellationToken cancellationToken)
            {

                DrinkCategory ?drinkCategory = await _drinkCategoryRepository.GetAsync(x => x.Id == request.AddDrinkToCategoryDto.DrinkCategoryId, x => x.Include(x => x.Menu.Resturant));
                await _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, drinkCategory.Menu.Resturant.Id);
                Drink drink = _mapper.Map<Drink>(request.AddDrinkToCategoryDto);
                drink.RestaurantId = drinkCategory.Menu.RestaurantId;

                Drink addedDrink = await _drinkRepository.CreateAsync(drink);

                CommandResponseDrinkDto response = _mapper.Map<CommandResponseDrinkDto>(addedDrink);
                return response;

            }
        }
    }
}
