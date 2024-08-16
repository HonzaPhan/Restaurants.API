using MediatR;

namespace Restaurants.Application.Commands.Restaurants
{
    public class DeleteRestaurantCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
