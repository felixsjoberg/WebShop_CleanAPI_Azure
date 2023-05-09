using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task<Product> GetProductAsync(Guid id);
}