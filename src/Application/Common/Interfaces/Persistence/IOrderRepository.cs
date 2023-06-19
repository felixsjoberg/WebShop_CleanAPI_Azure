using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order?> GetByIdNoTrackAsync(Guid id);
    Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);
    Task<IEnumerable<Order>> GetAllAsync();
    Task CreateAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Order order);
}