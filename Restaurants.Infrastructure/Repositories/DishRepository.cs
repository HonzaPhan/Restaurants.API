using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishRepository(AppDbContext dbContext) : IDishRepository
    {
        public Task SaveChanges() => dbContext.SaveChangesAsync();

        public async Task<int> CreateAsync(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await SaveChanges();
            return entity.Id;
        }

        public async Task DeleteAsync(IEnumerable<Dish> entities)
        {
            dbContext.Dishes.RemoveRange(entities);
            await SaveChanges();
        }

        public async Task DeleteByIdAsync(Dish entity)
        {
            dbContext.Dishes.Remove(entity);
            await SaveChanges();
        }
    }
}
