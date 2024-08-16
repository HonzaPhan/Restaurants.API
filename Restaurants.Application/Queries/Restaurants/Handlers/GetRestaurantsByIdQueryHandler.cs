using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs.Restaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Restaurants.Handlers
{
    public class GetRestaurantsByIdQueryHandler(ILogger<GetRestaurantsByIdQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository) : IRequestHandler<GetRestaurantsByIdQuery, RestaurantDto>
    {
        public async Task<RestaurantDto> Handle(GetRestaurantsByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting restaurant by ID {RestaurantId}", request.Id);
            Restaurant restaurant = await restaurantRepository.GetByIdAsync(request.Id) 
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            // Mapping the entities to DTOs
            RestaurantDto restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }
    }
}
