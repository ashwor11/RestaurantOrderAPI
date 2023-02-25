using Application.Features.Drinks.Dtos;
using Application.Features.Drinks.Rules;
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

namespace Application.Features.Drinks.Commands.UpdateDrink
{
    public class UpdateDrinkCommand : IRequest<CommandResponseDrinkDto>
    {
        public UpdateDrinkDto UpdateDrinkDto { get; set; }
        public int OwnerId { get; set; }

        public class UpdateDrinkCommandHandler : IRequestHandler<UpdateDrinkCommand, CommandResponseDrinkDto>
        {
            private readonly IDrinkRepository _drinkRepository;
            private readonly DrinkBusinessRules _drinkBusinessRules;
            private readonly IMapper _mapper;
            private readonly GeneralBusinessRules _businessRules;

            public UpdateDrinkCommandHandler(IDrinkRepository drinkRepository, DrinkBusinessRules drinkBusinessRules, IMapper mapper, GeneralBusinessRules businessRules)
            {
                _drinkRepository = drinkRepository;
                _drinkBusinessRules = drinkBusinessRules;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CommandResponseDrinkDto> Handle(UpdateDrinkCommand request, CancellationToken cancellationToken)
            {
                Drink ?drink = await _drinkRepository.GetAsync(x => x.Id == request.UpdateDrinkDto.Id, x => x.Include(x=>x.DrinkCategory).ThenInclude(x=>x.Menu));
                _drinkBusinessRules.DoesDrinkExists(drink);
                await _businessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, drink.DrinkCategory.Menu.RestaurantId);

                SetProperties(request.UpdateDrinkDto, drink);

                Drink updatedDrink = await _drinkRepository.UpdateAsync(drink);

                CommandResponseDrinkDto response = _mapper.Map<CommandResponseDrinkDto>(updatedDrink);

                return response;





            }

            private void SetProperties(UpdateDrinkDto updateDrinkDto, Drink drink)
            {
                PropertyInfo[] properties = typeof(UpdateDrinkDto).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    if (updateDrinkDto.GetType().GetProperty(property.Name).GetValue(updateDrinkDto) != null) typeof(Drink).GetProperty(property.Name).SetValue(drink, property.GetValue(updateDrinkDto));
                }
            }

        }
    }
}
