using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeactivateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetAsync(int id);
    Task<IEnumerable<Product>> SearchAsync();
    Task<CreateProductResult> AddAsync(Product product);
    Task<DeleteProductResult> DeleteAsync(int id);
    Task<DeactivateProductResult> DeactivateAsync(Product product);
    Task<UpdateProductResult> UpdateAsync(Product product);
}