using MediatR;
using Restaurants.Application.DTOs.Restaurant;

namespace Restaurants.Application.Queries.Restaurants
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {
    }
}
