using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<int> AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
}
