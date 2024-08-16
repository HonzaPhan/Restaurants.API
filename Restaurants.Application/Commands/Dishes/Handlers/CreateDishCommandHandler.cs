using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Dishes.Handlers
{
    public class CreateDishCommandHandler: IRequestHandler<CreateDishCommand, int>
    {
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishRepository _disheshRepository;
        private readonly IMapper _mapper;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, IRestaurantRepository restaurantRepository, IDishRepository disheshRepository, IMapper mapper)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
            _disheshRepository = disheshRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new dish: {@DishRequest}", request);

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId)
                ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            Dish dish = _mapper.Map<Dish>(request);

           return await _disheshRepository.CreateAsync(dish);
        }
    }
}
