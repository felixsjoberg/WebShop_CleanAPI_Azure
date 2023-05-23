using Application.Common.Interfaces.Persistence;
using Application.Products.Commands.CreateProduct;
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
    public async Task<Product?> GetByIdAsync(int id)
    {
        var productId = new ProductId(id);
        var result = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(productId));
        return result;
    }
    public async Task<Product?> GetByIdWithTrackingAsync(Product product)
    {
        var result = await _dbContext.Products.FindAsync(product);
        return result;
    }

    public async Task<Product?> GetByNameAsync(string name)
    {
        var result = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        return result;
    }
    public async Task<int> AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
        return product.Id.Value;
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var result = await _dbContext.Products
        .AsNoTracking()
        .Select(x => new Product(
            x.Id,
            x.Name,
            x.Description,
            x.Price,
            x.Stock,
            x.CategoryId,
            x.IsActive,
            x.ImageUrl
        ))
        .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<Product>> SearchAsync(string searchTerm, int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        var query = _dbContext.Products.AsNoTracking().AsQueryable();

        query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId);
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice);
        }

        // Execute the query and return the results
        var results = await query.ToListAsync();
        return results;
    }

    public async Task DeleteAsync(int id)
    {
        var productId = new ProductId(id);
        var product = await _dbContext.Products.FindAsync(productId);

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeactivateAsync(Product product)
    {
        _dbContext.Entry(product).Property(p => p.IsActive).IsModified = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product updatedProduct)
    {
        _dbContext.Entry(updatedProduct).State = EntityState.Modified;
        _dbContext.Products.Update(updatedProduct);
        await _dbContext.SaveChangesAsync();
    }
}