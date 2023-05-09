using Application.Common.Interfaces.Persistence;
using Domain.Entities;
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

    public async Task<Product> GetProductAsync(Guid id)
    {
        var result = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }
}