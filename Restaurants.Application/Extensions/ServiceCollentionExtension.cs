using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Commands.Identity;
using System.Reflection;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollentionExtension
    {
        public static void AddAplication(this IServiceCollection services)
        {
            // Add external libraries
            Assembly appAssembly = typeof(ServiceCollentionExtension).Assembly;
            services.AddMediatR(config => config.RegisterServicesFromAssembly(appAssembly));
            services.AddAutoMapper(appAssembly);
            services.AddValidatorsFromAssembly(appAssembly)
                .AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();

            services.AddHttpContextAccessor();
        }
    }
}
