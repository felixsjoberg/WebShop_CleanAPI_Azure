using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeactivateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product?> GetByIdWithTrackingAsync(Product product);
    Task<Product?> GetByNameAsync(string name);
    Task<IEnumerable<Product>> SearchAsync(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice);
    Task<int> AddAsync(Product product);
    Task DeleteAsync(int id);
    Task DeactivateAsync(Product product);
    Task UpdateAsync(Product product);
}