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
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Commands.DeleteDrink
{
    public class DeleteDrinkCommand : IRequest<CommandResponseDrinkDto>
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public class DeleteDrinkCommandHandler : IRequestHandler<DeleteDrinkCommand, CommandResponseDrinkDto>
        {
            private readonly IDrinkRepository _drinkRepository;
            private readonly GeneralBusinessRules _generealBusinessRules;
            private readonly DrinkBusinessRules _drinkBusinessRules;
            private readonly IMapper _mapper;

            public DeleteDrinkCommandHandler(IDrinkRepository drinkRepository, GeneralBusinessRules generealBusinessRules, DrinkBusinessRules drinkBusinessRules, IMapper mapper)
            {
                _drinkRepository = drinkRepository;
                _generealBusinessRules = generealBusinessRules;
                _drinkBusinessRules = drinkBusinessRules;
                _mapper = mapper;
            }

            public async Task<CommandResponseDrinkDto> Handle(DeleteDrinkCommand request, CancellationToken cancellationToken)
            {
                Drink? drink = await _drinkRepository.GetAsync(x => x.Id == request.Id, x=>x.Include(x=>x.DrinkCategory).ThenInclude(x=>x.Menu));
                _drinkBusinessRules.DoesDrinkExists(drink);
                await _generealBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, drink.DrinkCategory.Menu.RestaurantId);

                Drink? deletedDrink = await _drinkRepository.DeleteAsync(drink);
                CommandResponseDrinkDto response = _mapper.Map<CommandResponseDrinkDto>(deletedDrink);
                return response;

            }
        }
    }
}
