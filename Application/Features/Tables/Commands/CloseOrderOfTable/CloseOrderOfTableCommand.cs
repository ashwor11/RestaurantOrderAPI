using System.Security.Cryptography.X509Certificates;
using Application.Features.Tables.Dtos;
using Application.Features.Tables.Rules;
using Application.Services.Repositories;
using Application.Services.Rules;
using Application.Services.TableService;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tables.Commands.CloseOrderOfTable;

public class CloseOrderOfTableCommand : IRequest<ClosedOrderTableDto>
{
    public int Id { get; set; }
    public int OwnerId { get; set; }

    public class CloseOrderOfTableCommandHandler : IRequestHandler<CloseOrderOfTableCommand, ClosedOrderTableDto>
    {

        private readonly ITableRepository _tableRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly TableBusinessRules _tableBusinessRules;
        private readonly GeneralBusinessRules _generalBusinessRules;
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;

        public CloseOrderOfTableCommandHandler(ITableRepository tableRepository, TableBusinessRules tableBusinessRules, GeneralBusinessRules generalBusinessRules, ITableService tableService, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _tableBusinessRules = tableBusinessRules;
            _generalBusinessRules = generalBusinessRules;
            _tableService = tableService;
            _mapper = mapper;
        }

        public async Task<ClosedOrderTableDto> Handle(CloseOrderOfTableCommand request, CancellationToken cancellationToken)
        {
            Table table = await _tableRepository.GetAsync(x => x.Id == request.Id, x => x.Include(x => x.CurrentOrder).ThenInclude(x=>x.Products).ThenInclude(x=>x.Product));
            _tableBusinessRules.DoesTableExists(table);
            _generalBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId,table.RestaurantId);
            _tableBusinessRules.DoesTableHaveAnOpenOrder(table);

            Order closedOrder;
            Table closedOrderOfTable = _tableService.CloseTable(table, out closedOrder);

            Table updatedTable = await _tableRepository.UpdateAsync(table);

            ClosedOrderTableDto response = _mapper.Map<ClosedOrderTableDto>(updatedTable);
            response.ClosedOrder = _mapper.Map<ClosedOrderDto>(closedOrder);

            return response;




        }
    }
}