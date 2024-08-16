using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Restaurants.Handlers
{
    public class DeleteRestaurantCommandHandler: IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly ILogger<DeleteRestaurantCommand> _logger;
        private readonly IRestaurantRepository _restaurantRepository;

        public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommand> logger, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting restaurant with id: {RestaurantId}", request.Id);

            Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(request.Id) 
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            await _restaurantRepository.DeleteAsync(restaurant);
        }
    }
}
