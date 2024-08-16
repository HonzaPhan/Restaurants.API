using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.Dishes;
using Restaurants.Application.DTOs;
using Restaurants.Application.Queries.Dishes;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            int dishId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            IEnumerable<DishDto> dishes = await _mediator.Send(new GetAllDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDto dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<ActionResult<IEnumerable<DishDto>>> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await _mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public async Task<ActionResult<DishDto>> DeleteDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await _mediator.Send(new DeleteDishForRestaurantCommand(restaurantId, dishId));
            return NoContent();
        }
    }
}
