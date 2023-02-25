using Application.Features.DrinkCategories.Dtos;
using Application.Features.DrinkCategories.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.DrinkCategories.Queries.GetDrinkCategoryWithId;

public class GetDrinkCategoryWithIdCommand : IRequest<DrinkCategoryGetByIdDto>
{
    public int Id { get; set; }

    public class GetDrinkCategoryWithIdCommandHandler : IRequestHandler<GetDrinkCategoryWithIdCommand, DrinkCategoryGetByIdDto>
    {
        public GetDrinkCategoryWithIdCommandHandler(IDrinkCategoryRepository drinkCategoryRepository, DrinkCategoryBusinessRules drinkCategoryBusinessRules, IMapper mapper)
        {
            _drinkCategoryRepository = drinkCategoryRepository;
            _drinkCategoryBusinessRules = drinkCategoryBusinessRules;
            _mapper = mapper;
        }

        private readonly IDrinkCategoryRepository _drinkCategoryRepository;
        private readonly DrinkCategoryBusinessRules _drinkCategoryBusinessRules;
        private readonly IMapper _mapper;

        public async Task<DrinkCategoryGetByIdDto> Handle(GetDrinkCategoryWithIdCommand request, CancellationToken cancellationToken)
        {
            DrinkCategory? drinkCategory =
                await _drinkCategoryRepository.GetAsync(x => x.Id == request.Id, x => x.Include(x => x.Drinks), cancellationToken:cancellationToken);
            _drinkCategoryBusinessRules.DoesDrinkCategoryExists(drinkCategory);
            DrinkCategoryGetByIdDto response = _mapper.Map<DrinkCategoryGetByIdDto>(drinkCategory);
            return response;
        }
    }
}