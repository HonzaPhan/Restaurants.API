using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs.Restaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Restaurants.Handlers
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            IEnumerable<Restaurant> restaurants = await restaurantRepository.GetAllAsync();

            // Mapping the entities to DTOs
            IEnumerable<RestaurantDto> restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantDto;
        }
    }
}
