using MediatR;

namespace Restaurants.Application.Commands.Dishes
{
    public class DeleteDishForRestaurantCommand(int restaurantId, int dishId) : IRequest
    {
        public int RestaurantId { get; } = restaurantId;
        public int DishId { get; } = dishId;
    }
}
