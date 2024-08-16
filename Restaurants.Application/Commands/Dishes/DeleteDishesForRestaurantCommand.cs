using MediatR;

namespace Restaurants.Application.Commands.Dishes
{
    public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
