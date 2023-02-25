using Application.Features.Tables.Dtos;
using Application.Features.Tables.Rules;
using Application.Services.ProductService;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tables.Commands.PayPriceOfProduct;

public class PayPriceOfProductCommand : IRequest<TableInOrderDto>
{
    public PayProductsOfTableDto PayProductsOfTableDto { get; set; }

    public int OwnerId { get; set; }

    public class PayPriceOfProductCommandHandler : IRequestHandler<PayPriceOfProductCommand, TableInOrderDto>
    {
        private readonly IProductService _productService;
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;
        private readonly GeneralBusinessRules _generalBusinessRules;
        private readonly TableBusinessRules _tableBusinessRules;

        public PayPriceOfProductCommandHandler(IProductService productService, ITableRepository tableRepository, IMapper mapper, GeneralBusinessRules generalBusinessRules, TableBusinessRules tableBusinessRules)
        {
            _productService = productService;
            _tableRepository = tableRepository;
            _mapper = mapper;
            _generalBusinessRules = generalBusinessRules;
            _tableBusinessRules = tableBusinessRules;
        }

        public async Task<TableInOrderDto> Handle(PayPriceOfProductCommand request, CancellationToken cancellationToken)
        {
            Table ?table = await _tableRepository.GetAsync(x => x.Id == request.PayProductsOfTableDto.TableId, x=>x.Include(x=>x.CurrentOrder).ThenInclude(x=>x.Products).ThenInclude(x=>x.Product));
            _tableBusinessRules.DoesTableExists(table);
            await _generalBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, table.RestaurantId);
            _tableBusinessRules.DoesTableHaveAnOpenOrder(table);

            _productService.PayProductsPrice(table, request.PayProductsOfTableDto.ProductIds);
            Table updatedTable = await _tableRepository.UpdateAsync(table);

            TableInOrderDto response = _mapper.Map<TableInOrderDto>(table);
            return response;
        }
    }
}