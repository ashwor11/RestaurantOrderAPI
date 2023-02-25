using Application.Features.Categories.Commands.AddCategoryWithRestaurantId;
using Application.Features.Categories.Dtos;
using Application.Features.FoodCategories.Commands.DeleteFoodCategory;
using Application.Features.FoodCategories.Commands.UpdateFoodCategory;
using Application.Features.FoodCategories.Queries.GetFoodCategoryById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddFoodCategory([FromBody] CreateCategoryWithRestaurantIdDto requestDto)
        {
            int ownerId = GetOwnerId();
            AddFoodCategoryWithRestaurantIdCommand request = new() { Name = requestDto.Name, RestaurantId = requestDto.RestaurantId, OwnerId = ownerId };
            CreatedFoodCategoryDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateFoodCategory([FromBody] UpdateFoodCategoryDto updateFoodCategoryDto)
        {
            int ownerId = GetOwnerId();
            UpdateFoodCategoryCommand request = new()
                { OwnerId = ownerId, UpdateFoodCategoryDto = updateFoodCategoryDto };
            UpdatedFoodCategoryDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFoodCategory([FromBody] int foodCategoryId)
        {
            int ownerId = GetOwnerId();
            DeleteFoodCategoryCommand request = new() { OwnerId = ownerId, Id = foodCategoryId };
            DeletedFoodCategoryDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{foodCategoryId}")]
        public async Task<IActionResult> GetFoodCategory([FromRoute] int foodCategoryId)
        {
            GetFoodCategoryByIdQuery request = new() { Id = foodCategoryId };
            GetFoodCategoryByIdDto response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
