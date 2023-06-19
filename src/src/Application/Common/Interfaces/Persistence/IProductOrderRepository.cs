using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;
public interface IProductOrderRepository
{
    Task<ProductOrder?> GetByOrderIdAsync(int orderId);
    Task<ProductOrder?> GetByProductIdAsync(int id);
    Task CreateAsync(ProductOrder productOrder);
    Task UpdateAsync(ProductOrder existingProductOrder);
}