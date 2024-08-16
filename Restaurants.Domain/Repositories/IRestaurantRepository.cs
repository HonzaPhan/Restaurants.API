using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> CreateAsync(Restaurant entity);
        Task DeleteAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task SaveChanges();
    }
}
