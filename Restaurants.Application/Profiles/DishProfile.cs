using AutoMapper;
using Restaurants.Application.Commands.Dishes;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<Dish, DishDto>();
        }
    }
}
