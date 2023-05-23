using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;
public interface IProductOrderRepository
{
    Task CreateAsync(ProductOrder productOrder);
}