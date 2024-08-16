using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Dishes.Handlers
{
    public class DeleteDishesForRestaurantCommandHandler : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        private readonly IDishRepository _dishRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger<DeleteDishesForRestaurantCommandHandler> _logger;

        public DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger, IDishRepository dishRepository, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _dishRepository = dishRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning("Deleting all dishes for restaurant with id: {RestaurantId}", request.RestaurantId);

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            await _dishRepository.DeleteAsync(restaurant.Dishes);
        }
    }
}
