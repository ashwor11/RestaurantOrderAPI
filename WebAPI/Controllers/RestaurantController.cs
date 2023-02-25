using Application.Features.Restaurants.Commands.CreateRestaurant;
using Application.Features.Restaurants.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : BaseController
    {
        
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] string restaurantName)
        {
            CreateRestaurantCommand request = new() { OwnerId = GetOwnerId(), RestaurantName = restaurantName };
            CreatedRestaurantResponseDto response = await Mediator.Send(request);

            return Ok(response);
        }
    }
}
