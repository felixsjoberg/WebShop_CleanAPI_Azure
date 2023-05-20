using Application.Common.Interfaces.Persistence;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeactivateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly TopStyleDbContext _dbContext;
    public ProductRepository(TopStyleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<Product?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CreateProductResult> AddAsync(Product product)
    {
        throw new NotImplementedException();
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var result = await _dbContext.Products.ToListAsync();
        return result;
    }
    public async Task<Product?> GetProductAsync(ProductId id)
    {
        var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }

    public async Task<IEnumerable<Product>> SearchAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<DeleteProductResult> DeleteAsync(int id)
    {
        var productId = new ProductId(id);
        var product = await _dbContext.Products.FindAsync(productId);
        if (product == null)
            return new DeleteProductResult(false);

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        return new DeleteProductResult(true);

    }

    public Task<DeactivateProductResult> DeactivateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateProductResult> UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}