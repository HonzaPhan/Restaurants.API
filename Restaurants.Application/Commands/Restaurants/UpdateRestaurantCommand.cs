using MediatR;

namespace Restaurants.Application.Commands.Restaurants
{
    public class UpdateRestaurantCommand(int id): IRequest
    {
        public int Id { get; } = id;
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }
    }
}
