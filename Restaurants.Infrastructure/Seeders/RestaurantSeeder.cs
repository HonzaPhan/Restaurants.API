using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(AppDbContext context) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await context.Database.CanConnectAsync() && !context.Restaurants.Any())
            {
                IEnumerable<Restaurant> restaurants = GetRestaurants();
                context.Restaurants.AddRange(restaurants);
                await context.SaveChangesAsync();
            }

            if (!context.Roles.Any())
            {
                IEnumerable<IdentityRole> roles = GetRoles();
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }

        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
            [
                new IdentityRole
                {
                    Name = UserRoles.Admin,
                    NormalizedName = UserRoles.Admin.ToUpper()
                },
                new IdentityRole
                {
                    Name = UserRoles.Owner,
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new IdentityRole
                {
                    Name = UserRoles.User,
                    NormalizedName = UserRoles.User.ToUpper()
                }
            ];

            return roles;
        }

        private IEnumerable<Restaurant> GetRestaurants()
            {
            List<Restaurant> restaurants = [
            new()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description =
                    "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Nashville Hot Chicken",
                        Description = "Nashville Hot Chicken (10 pcs.)",
                        Price = 10.30M,
                    },

                    new ()
                    {
                        Name = "Chicken Nuggets",
                        Description = "Chicken Nuggets (5 pcs.)",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    ZipCode = "WC2N 5DU"
                },

            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description =
                    "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Boots 193",
                    ZipCode = "W1F 8SR"
                }
            },
            new ()
            {
                Name = "Pizza Hut",
                Category = "Fast Food",
                Description =
                    "Pizza Hut is an American restaurant chain and international franchise founded in 1958 in Wichita, Kansas by Dan and Frank Carney.",
                ContactEmail = "pizza@hut.com",
                HasDelivery = false,
                Address = new Address()
                {
                    City = "London",
                    Street = "Oxford St",
                    ZipCode = "W1D 1BS"
                },
                Dishes =
                [
                    new ()
                    {
                        Name = "Pepperoni Pizza",
                        Description = "Pepperoni Pizza (Medium)",
                        Price = 12.30M,
                    },

                    new ()
                    {
                        Name = "Margherita Pizza",
                        Description = "Margherita Pizza (Medium)",
                        Price = 10.30M,
                    },
                ]
            },
            new ()
            {
                Name = "Five Guys",
                Category = "Fast Food",
                Description =
                    "Five Guys Enterprises LLC (doing business as Five Guys Burgers and Fries) is an American fast casual restaurant chain focused on hamburgers, hot dogs, and French fries.",
                ContactEmail = "five@guys.com",
                HasDelivery = true,
                Address = new Address()
                {
                    City = "London",
                    Street = "Long Acre",
                    ZipCode = "WC2E 9LH"
                },
                Dishes =
                [
                    new ()
                    {
                        Name = "Bacon Cheeseburger",
                        Description = "Bacon Cheeseburger",
                        Price = 8.30M,
                    },

                    new ()
                    {
                        Name = "Fries",
                        Description = "Fries",
                        Price = 4.30M,
                    },
                ],
            }
        ];

            return restaurants;
        }
    }
}
