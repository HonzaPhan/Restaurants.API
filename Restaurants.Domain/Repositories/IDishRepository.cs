using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishRepository
    {
        Task<int> CreateAsync(Dish entity);
        Task DeleteAsync(IEnumerable<Dish> entities);
        Task DeleteByIdAsync(Dish entity);
        Task SaveChanges();
    }
}
