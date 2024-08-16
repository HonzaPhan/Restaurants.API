using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Dishes
{
    public class GetAllDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
