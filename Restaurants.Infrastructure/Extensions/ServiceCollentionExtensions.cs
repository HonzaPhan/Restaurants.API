using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollentionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(connectionString)
                // Enable sensitive data logging
                // .EnableSensitiveDataLogging()
            );

            // Add identity
            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<AppDbContext>();

            // Add seeders
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();

            // Add repositories
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();

            // Add authorization handlers
            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(PolicyNames.HasNationality, "Czech", "English"))
                .AddPolicy(PolicyNames.AtLeast20YearsOld, builder => builder.AddRequirements(new MinimumAgeRequirement(10)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        }
    }
}
