using Application.Common.Interfaces.Persistence;
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

    public async Task<Product?> GetProductAsync(ProductId id)
    {
        var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }
}