using Application.Features.Foods.Commands.AddFoodToCategory;
using Application.Features.Foods.Commands.DeleteFood;
using Application.Features.Foods.Commands.UpdateFood;
using Application.Features.Foods.Dtos;
using Application.Features.Foods.Queries.GetFoodById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddFoodToCategory([FromBody] AddFoodToCategoryDto addFoodToCategoryDto)
        {
            int ownerId = GetOwnerId();
            AddFoodToCategoryCommand request = new() { AddFoodToCategoryDto = addFoodToCategoryDto, OwnerId = ownerId };
            CommandResponseFoodDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateFood([FromBody] UpdateFoodDto updateFoodDto)
        {
            int ownerId = GetOwnerId();
            UpdateFoodCommand request = new() { OwnerId = ownerId, UpdateFoodDto = updateFoodDto };
            CommandResponseFoodDto response = await Mediator.Send(request);
            return Ok(response);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFood([FromBody] int id)
        {
            int ownerId = GetOwnerId();
            DeleteFoodCommand request = new() { OwnerId = ownerId, Id = id };
            CommandResponseFoodDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{foodId}")]
        public async Task<IActionResult> GetFoodById([FromRoute] int foodId)
        {
            GetFoodByIdCommand request = new() { Id = foodId };
            GetFoodByIdDto response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
