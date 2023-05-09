using Domain.Entities;

namespace BankApplication.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
}
