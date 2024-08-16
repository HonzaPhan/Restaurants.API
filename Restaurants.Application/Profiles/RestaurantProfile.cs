using AutoMapper;
using Restaurants.Application.Commands.Restaurants;
using Restaurants.Application.DTOs.Restaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();

            CreateMap<CreateRestaurantCommand, Restaurant>()
                .ForMember(d => d.Address, options => options.MapFrom(src => new Address
                {
                    City = src.City,
                    ZipCode = src.ZipCode,
                    Street = src.Street
                }));

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(d => d.City, options =>
                    options.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.ZipCode, options =>
                    options.MapFrom(src => src.Address == null ? null : src.Address.ZipCode))
                .ForMember(d => d.Street, options =>
                    options.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.Dishes, options => options.MapFrom(src => src.Dishes));
        }
    }
}
