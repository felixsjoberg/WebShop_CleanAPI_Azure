using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(int id);
    Task<Address?> GetByIdNoTrackingAsync(int id);
    Task<IEnumerable<Address>> GetAllAsync();
    Task<int> AddAsync(Address address);
    Task DeleteAsync(Address address);
    Task UpdateAsync(Address address);
}