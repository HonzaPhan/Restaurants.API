using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantRepository(AppDbContext dbContext) : IRestaurantRepository
    {
        public Task SaveChanges() => dbContext.SaveChangesAsync();

        public async Task<int> CreateAsync(Restaurant entity)
        {
            dbContext.Restaurants.Add(entity);
            await SaveChanges();
            return entity.Id;
        }

        public async Task DeleteAsync(Restaurant entity)
        {
            dbContext.Restaurants.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            List<Restaurant> restaurant = await dbContext.Restaurants
                .ToListAsync();
            return restaurant;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            Restaurant? restaurant = await dbContext.Restaurants
                .Include(d => d.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            dbContext.Restaurants.Update(restaurant);
            await SaveChanges();
        }
    }
}
