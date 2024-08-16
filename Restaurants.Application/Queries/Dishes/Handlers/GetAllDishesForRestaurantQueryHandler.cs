using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Dishes.Handlers
{
    public class GetAllDishesForRestaurantQueryHandler : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        private readonly ILogger<GetAllDishesForRestaurantQueryHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetAllDishesForRestaurantQueryHandler(ILogger<GetAllDishesForRestaurantQueryHandler> logger, IRestaurantRepository restaurantRepository, IDishRepository dishRepository, IMapper mapper)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all dishes for a restaurant with ID {RestaurantId}", request.RestaurantId);

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            IEnumerable<DishDto> dishes = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);

            return dishes;
        }
    }
}
