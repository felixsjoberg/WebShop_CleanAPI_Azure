using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductOrderRepository : IProductOrderRepository
{
    private readonly TopStyleDbContext _dbContext;

    public ProductOrderRepository(TopStyleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ProductOrder?> GetByOrderIdAsync(int orderId)
    {
        var result = await _dbContext.ProductOrders.AsNoTracking().FirstOrDefaultAsync(x => x.OrderId.Equals(orderId));
        return result;
    }
    public async Task<ProductOrder?> GetByProductIdAsync(int id)
    {
        var result = await _dbContext.ProductOrders.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId.Equals(id));
        return result;
    }
    public async Task CreateAsync(ProductOrder productOrder)
    {
        await _dbContext.ProductOrders.AddAsync(productOrder);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductOrder productOrder)
    {
        _dbContext.Entry(productOrder).State = EntityState.Modified;
        _dbContext.ProductOrders.Update(productOrder);
        await _dbContext.SaveChangesAsync();
    }
}