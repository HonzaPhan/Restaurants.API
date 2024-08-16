using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Dishes.Handlers
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        private readonly ILogger<GetAllDishesForRestaurantQueryHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IMapper _mapper;

        public GetDishByIdForRestaurantQueryHandler(ILogger<GetAllDishesForRestaurantQueryHandler> logger, IRestaurantRepository restaurantRepository, IDishRepository dishRepository, IMapper mapper)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _dishRepository = dishRepository;
            _mapper = mapper;
        }

        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving dish: {DishId} for a restaurant with ID {RestaurantId}", request.DishId, request.RestaurantId);

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId)
                ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            DishDto dishDto = _mapper.Map<DishDto>(dish);

            return dishDto;
        }
    }
}
