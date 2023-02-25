using Application.Features.Drinks.Dtos;
using Application.Features.Drinks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Drinks.Queries.GetDrinkById
{
    public class GetDrinkByIdCommand : IRequest<DrinkGetByIdDto>
    {
        public int Id { get; set; }

        public class GetDrinkByIdCommandHandler : IRequestHandler<GetDrinkByIdCommand, DrinkGetByIdDto>
        {
            private readonly IDrinkRepository _drinkRepository;
            private readonly DrinkBusinessRules _drinkBusinessRules;
            private readonly IMapper _mapper;

            public GetDrinkByIdCommandHandler(IDrinkRepository drinkRepository, DrinkBusinessRules drinkBusinessRules, IMapper mapper)
            {
                _drinkRepository = drinkRepository;
                _drinkBusinessRules = drinkBusinessRules;
                _mapper = mapper;
            }

            public async Task<DrinkGetByIdDto> Handle(GetDrinkByIdCommand request, CancellationToken cancellationToken)
            {
                Drink? drink = await _drinkRepository.GetAsync(x => x.Id == request.Id);
                _drinkBusinessRules.DoesDrinkExists(drink);
                DrinkGetByIdDto response = _mapper.Map<DrinkGetByIdDto>(drink);
                return response;

            }
        }
    }
}
