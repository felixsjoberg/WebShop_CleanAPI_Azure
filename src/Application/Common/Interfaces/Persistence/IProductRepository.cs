using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByIdNoTrackingAsync(Product product);
    Task<Product?> GetByNameAsync(string name);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice);
    Task<int> AddAsync(Product product);
    Task DeleteAsync(Product product);
    Task DeactivateAsync(Product product);
    Task UpdateAsync(Product product);
    Task UpdateAsyncWithTrack(Product product);
}