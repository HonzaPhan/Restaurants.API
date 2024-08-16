using MediatR;
using Restaurants.Application.DTOs.Restaurant;

namespace Restaurants.Application.Queries.Restaurants
{
    public class GetRestaurantsByIdQuery(int id) : IRequest<RestaurantDto>
    {
        public int Id { get; } = id;
    }
}
