using Application.Features.Tables.Commands.AddProductToOrder;
using Application.Features.Tables.Commands.CloseOrderOfTable;
using Application.Features.Tables.Commands.CreateTable;
using Application.Features.Tables.Commands.PayPriceOfProduct;
using Application.Features.Tables.Dtos;
using Application.Features.Tables.Queries.GetTableById;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddTableToRestaurant([FromBody] CreateTableDto createTableDto)
        {
            int ownerId = GetOwnerId();
            CreateTableCommand request = new() { CreateTableDto = createTableDto, OwnerId = ownerId };
            CreatedTableDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductsToRestaurant([FromBody] AddProductToOrderDto addProductToOrderDto)
        {
            int ownerId = GetOwnerId();
            AddProductsToOrderCommand request = new() { AddProductToOrderDto = addProductToOrderDto, OwnerId = ownerId };
            TableInOrderDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{tableId}")]
        public async Task<IActionResult> GetTableById([FromRoute] int tableId)
        {
            GetTableByIdCommand request = new() { Id = tableId };
            GetTableByIdDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("closeOrder")]
        public async Task<IActionResult> CloseOrderByTableId([FromBody] int id)
        {
            int ownerId = GetOwnerId();
            CloseOrderOfTableCommand request = new() { Id = id, OwnerId = ownerId};
            ClosedOrderTableDto response = await Mediator.Send(request);
            return Ok(response);

        }

        [HttpPost("payProduct")]
        public async Task<IActionResult> PayProductOfTable([FromBody] PayProductsOfTableDto payProductsOfTableDto)
        {
            int ownerId = GetOwnerId();
            PayPriceOfProductCommand request = new()
                { OwnerId = ownerId, PayProductsOfTableDto = payProductsOfTableDto };
            TableInOrderDto response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
