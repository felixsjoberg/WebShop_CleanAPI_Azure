using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly TopStyleDbContext _dbContext;
    public OrderRepository(TopStyleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var orderId = new OrderId(id);
        var result = await _dbContext.Orders
        .Include(o => o.Customer)
        .Include(o => o.ProductOrders)
        .ThenInclude(po => po.Product)
        .FirstOrDefaultAsync(o => o.Id.Equals(orderId));
        return result;
    }
    public async Task<Order?> GetByIdNoTrackAsync(Guid id)
    {
        var orderId = new OrderId(id);
        var result = await _dbContext.Orders
        .AsNoTracking()
        .Include(o => o.Customer)
        .Include(o => o.ProductOrders)
        .ThenInclude(po => po.Product)
        .FirstOrDefaultAsync(o => o.Id.Equals(orderId));
        return result;
    }
    public async Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId)
    {
        return await _dbContext.Orders
        .AsNoTracking()
        .Include(o => o.ShippingAddress)
        .Include(o => o.Customer)
        .Include(o => o.ProductOrders)
        .ThenInclude(po => po.Product)
        .OrderByDescending(o => o.OrderDate)
        .Where(x=>x.Customer.UserId == userId.ToString())
        .ToListAsync();
    }
    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _dbContext.Orders
        .AsNoTracking()
        .Include(o => o.ShippingAddress)
        .Include(o => o.Customer)
        .Include(o => o.ProductOrders)
        .ThenInclude(po => po.Product)
        .OrderByDescending(o => o.OrderDate)
        .ToListAsync();
    }
    public async Task CreateAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Order order)
    {
        _dbContext.Orders.Entry(order).CurrentValues.SetValues(order);
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Order order)
    {
        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}