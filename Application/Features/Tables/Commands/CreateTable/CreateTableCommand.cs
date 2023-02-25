using System.Security.AccessControl;
using Application.Features.Tables.Dtos;
using Application.Services.Repositories;
using Application.Services.RestaurantService;
using Application.Services.Rules;
using Application.Services.TableService;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tables.Commands.CreateTable;

public class CreateTableCommand : IRequest<CreatedTableDto>, ILoggableRequest
{
    public CreateTableDto CreateTableDto { get; set; }
    public int OwnerId { get; set; }

    public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, CreatedTableDto>
    {
        

        private readonly IRestaurantService _restaurantService;
        private readonly ITableRepository _tableRepository;
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;
        private readonly GeneralBusinessRules _generalBusinessRules;

        public CreateTableCommandHandler(IRestaurantService restaurantService, ITableRepository tableRepository, ITableService tableService, IMapper mapper, GeneralBusinessRules generalBusinessRules)
        {
            _restaurantService = restaurantService;
            _tableRepository = tableRepository;
            _tableService = tableService;
            _mapper = mapper;
            _generalBusinessRules = generalBusinessRules;
        }

        public async Task<CreatedTableDto> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            await _restaurantService.DoesRestaurantExists(request.CreateTableDto.RestaurantId);
            await _generalBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId,
                request.CreateTableDto.RestaurantId);
            await _tableService.DoesRestaurantHaveTableWithThisNo(request.CreateTableDto.RestaurantId,
                request.CreateTableDto.TableNo);

            Table table = new()
                { RestaurantId = request.CreateTableDto.RestaurantId, TableNo = request.CreateTableDto.TableNo };
            Table addedTable = await _tableRepository.CreateAsync(table);
            CreatedTableDto response = _mapper.Map<CreatedTableDto>(addedTable);
            return response;


        }
    }
}