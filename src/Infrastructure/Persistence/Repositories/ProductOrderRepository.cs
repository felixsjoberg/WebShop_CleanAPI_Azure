using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DataContext;

namespace Infrastructure.Persistence.Repositories;

public class ProductOrderRepository : IProductOrderRepository
{
    private readonly TopStyleDbContext _dbContext;

    public ProductOrderRepository(TopStyleDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(ProductOrder productOrder)
    {
        await _dbContext.ProductOrders.AddAsync(productOrder);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductOrder productOrder)
    {
        // _dbContext.Entry(productOrder).State = EntityState.Modified;
        _dbContext.ProductOrders.Update(productOrder);
        await _dbContext.SaveChangesAsync();
    }
}