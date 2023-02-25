using Application.Features.Restaurants.Commands.CreateRestaurant;
using Application.Features.Restaurants.Dtos;
using Application.Features.Restaurants.Models;
using Application.Features.Restaurants.Queries.GetAllRestaurants;
using Application.Features.Restaurants.Queries.GetRestaurantWithAllDetails;
using Core.Application.Requests;
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllRestaurant([FromQuery] PageRequest pageRequest)
        {
            GetAllRestaurantsQuery query = new() { PageNumber = pageRequest.PageNumber, PageSize = pageRequest.PageSize};
            GetAllRestaurantsModel response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetRestaurantWithDetails([FromRoute] int restaurantId)
        {
            GetRestaurantWithAllDetailsQuery query = new() { RestaurantId = restaurantId };
            RestaurantWithDetailsDto response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
