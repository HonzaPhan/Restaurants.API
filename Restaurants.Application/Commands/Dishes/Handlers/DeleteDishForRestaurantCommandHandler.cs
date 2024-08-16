using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Dishes.Handlers
{
    public class DeleteDishForRestaurantCommandHandler : IRequestHandler<DeleteDishForRestaurantCommand>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;

        public DeleteDishForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IDishRepository dishRepository, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _dishRepository = dishRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(DeleteDishForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Deleting dish with id: {DishId} for restaurant with id: {RestaurantId}", request.DishId, request.RestaurantId);

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            Dish dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId)
                ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            await _dishRepository.DeleteByIdAsync(dish);
        }
    }
}
