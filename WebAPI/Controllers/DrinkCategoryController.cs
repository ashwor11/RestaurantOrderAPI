using Application.Features.Categories.Commands.AddCategoryWithRestaurantId;
using Application.Features.Categories.Dtos;
using Application.Features.DrinkCategories.Commands.AddDrinkCategoryWithRestaurantId;
using Application.Features.DrinkCategories.Commands.DeleteDrinkCategory;
using Application.Features.DrinkCategories.Commands.UpdateDrinkCategory;
using Application.Features.DrinkCategories.Dtos;
using Application.Features.DrinkCategories.Queries.GetDrinkCategoryWithId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkCategoryController : BaseController
    {

        
        [HttpPost("add")]
        public async Task<IActionResult> AddDrinkCategory([FromBody] CreateDrinkCategoryWithRestaurantIdDto requestDto)
        {
            int ownerId = GetOwnerId();
            AddDrinkCategoryWithRestaurantIdCommand request = new() {CreateDrinkCategoryWithRestaurantIdDto = requestDto, OwnerId = ownerId  };
            CreatedDrinkCategoryDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDrinkCategory([FromBody] UpdateDrinkCategoryDto requestDto)
        {
            int ownerId = GetOwnerId();
            UpdateDrinkCategoryCommand request = new() { OwnerId = ownerId, UpdateDrinkCategoryDto = requestDto};
            UpdatedDrinkCategoryDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDrinkCategory([FromBody] int drinkCategoryId)
        {
            int ownerId = GetOwnerId();
            DeleteDrinkCategoryCommand request = new() { OwnerId = ownerId, Id = drinkCategoryId };
            DeletedDrinkCategoryDto response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{drinkCategoryId}")]
        public async Task<IActionResult> GetDrinkCategoryById([FromRoute] int drinkCategoryId)
        {
            GetDrinkCategoryWithIdCommand request = new() { Id = drinkCategoryId };
            DrinkCategoryGetByIdDto response = await Mediator.Send(request);
            return Ok(response);
        }
    }
}
