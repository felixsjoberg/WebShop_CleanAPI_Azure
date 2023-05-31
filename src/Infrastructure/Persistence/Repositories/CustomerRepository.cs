using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly TopStyleDbContext _dbContext;

    public CustomerRepository(TopStyleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Customer?> GetByIdAsync(int id)
    {
        var result = await _dbContext.Customers.FindAsync(id);
        return result;
    }
    public async Task<Customer?> GetByIdNoTrackAsync(int id)
    {
        var result = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return result;
    }
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var result = await _dbContext.Customers
        .AsNoTracking()
        .ToListAsync();

        return result;
    }
    public async Task<int> AddAsync(Customer customer)
    {
        await _dbContext.Customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
        return customer.Id;
    }
    public async Task UpdateAsync(Customer customer)
    {
        _dbContext.Entry(customer).State = EntityState.Modified;
        _dbContext.Customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Customer?> FindByUserIdAsync(string userId)
    {
        var customer = await _dbContext.Customers
            .Include(c => c.Address)
            .SingleOrDefaultAsync(c => c.UserId == userId);
        return customer;
    }
}