using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Common.Interfaces.Persistence;

public interface IProductRepository
{
    Task<Product?> GetProductAsync(ProductId id);
}