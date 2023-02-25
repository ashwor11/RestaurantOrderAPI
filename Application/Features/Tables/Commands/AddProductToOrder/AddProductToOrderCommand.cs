using Application.Features.Tables.Dtos;
using Application.Features.Tables.Rules;
using Application.Helpers;
using Application.Services.OrderService;
using Application.Services.ProductService;
using Application.Services.Repositories;
using Application.Services.Rules;
using AutoMapper;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using Domain.Entities.AbstractEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.Tables.Commands.AddProductToOrder;

public class AddProductsToOrderCommand : IRequest<TableInOrderDto>, ILoggableRequest
{
    public AddProductToOrderDto AddProductToOrderDto { get; set; }
    public int OwnerId { get; set; }

    public class AddProductsToOrderCommandHandler : IRequestHandler<AddProductsToOrderCommand,TableInOrderDto>
    {
        

        public AddProductsToOrderCommandHandler(ITableRepository tableRepository, IOrderedProductRepository orderedProductRepository, TableBusinessRules tableBusinessRules, IProductService productService, GeneralBusinessRules generalBusinessRules, IMapper mapper, IOrderService orderService)
        {
            _tableRepository = tableRepository;
            _tableBusinessRules = tableBusinessRules;
            _productService = productService;
            _generalBusinessRules = generalBusinessRules;
            _mapper = mapper;
        }

        private readonly ITableRepository _tableRepository;
        private readonly TableBusinessRules _tableBusinessRules;
        private readonly IProductService _productService;
        private readonly GeneralBusinessRules _generalBusinessRules;
        private readonly IMapper _mapper;

        public async Task<TableInOrderDto> Handle(AddProductsToOrderCommand request, CancellationToken cancellationToken)
        {
            Table? table = await _tableRepository.GetAsync(x => x.Id == request.AddProductToOrderDto.TableId, x=> x.Include(x=>x.CurrentOrder).ThenInclude(x=>x.Products).ThenInclude(x=>x.Product), cancellationToken: cancellationToken);

            _tableBusinessRules.DoesTableExists(table);
            await _generalBusinessRules.IsOwnerResponsibleForRestaurant(request.OwnerId, table.RestaurantId);

            List<Product> products = await _productService.GetSpecifiedProducts(request.AddProductToOrderDto.ProductIds);

            _productService.DoProductsBelongRestaurant(products, table.RestaurantId);

            _productService.AddProductsToCurrentOrder(products,table,request.AddProductToOrderDto.ProductIds);


            Table updatedTable = await _tableRepository.UpdateAsync(table);

            TableInOrderDto response = _mapper.Map<TableInOrderDto>(updatedTable);

            return response;

        }

        
    }

}

