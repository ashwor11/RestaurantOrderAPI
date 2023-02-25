using Application.Features.Foods.Dtos;
using Application.Features.Foods.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Foods.Queries.GetFoodById;

public class GetFoodByIdCommand : IRequest<GetFoodByIdDto>
{
    public int Id { get; set; }

    public class GetFoodByIdCommandHandler : IRequestHandler<GetFoodByIdCommand, GetFoodByIdDto>
    {
        

        private readonly IFoodRepository _foodRepository;
        private readonly FoodBusinessRules _foodBusinessRules;
        private readonly IMapper _mapper;

        public GetFoodByIdCommandHandler(IFoodRepository foodRepository, FoodBusinessRules foodBusinessRules, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _foodBusinessRules = foodBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetFoodByIdDto> Handle(GetFoodByIdCommand request, CancellationToken cancellationToken)
        {
            Food? food = await _foodRepository.GetAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            _foodBusinessRules.DoesFoodExists(food);
            GetFoodByIdDto response = _mapper.Map<GetFoodByIdDto>(food);
            return response;

        }
    }
}