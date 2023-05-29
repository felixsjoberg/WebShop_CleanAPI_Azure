using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<int> AddAsync(Address address);
}