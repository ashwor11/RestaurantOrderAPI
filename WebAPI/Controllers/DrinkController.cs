using Application.Features.Drinks.Commands.AddDrinkToCategory;
using Application.Features.Drinks.Commands.DeleteDrink;
using Application.Features.Drinks.Commands.UpdateDrink;
using Application.Features.Drinks.Dtos;
using Application.Features.Drinks.Queries.GetDrinkById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddDrinkToCategory([FromBody] AddDrinkToCategoryDto addDrinkToCategoryDto)
        {
            int ownerId = GetOwnerId();
            AddDrinkToCategoryCommand request = new() { AddDrinkToCategoryDto = addDrinkToCategoryDto, OwnerId = ownerId};
            CommandResponseDrinkDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDrink([FromBody] UpdateDrinkDto updateDrinkDto)
        {
            int ownerId = GetOwnerId();
            UpdateDrinkCommand request = new() { UpdateDrinkDto = updateDrinkDto, OwnerId = ownerId};
            CommandResponseDrinkDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDrink([FromBody] int id)
        {
            int ownerId = GetOwnerId();
            DeleteDrinkCommand request = new() { Id = id , OwnerId = ownerId};
            CommandResponseDrinkDto response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpGet("{drinkId}")]
        public async Task<IActionResult> GetDrink([FromRoute] int drinkId)
        {
            GetDrinkByIdCommand request = new() { Id = drinkId };
            DrinkGetByIdDto response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
