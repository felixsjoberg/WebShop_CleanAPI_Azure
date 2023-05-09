using BankApplication.Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Base;
public class UserRepository : IUserRepository
{
    private readonly TopStyleDbContext _context;

    public UserRepository(TopStyleDbContext context) 
    {
        _context = context;
    }
    // public async Task<IEnumerable<T>> GetAllAsync()
    // {

    // }
    public async Task<User> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}