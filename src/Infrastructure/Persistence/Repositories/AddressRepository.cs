using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly TopStyleDbContext _dbContext;

        public AddressRepository(TopStyleDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Address?> GetByIdAsync(int id)
        {
            var result = await _dbContext.Addresses
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<Address?> GetByIdNoTrackingAsync(int id)
        {
            var result = await _dbContext.Addresses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task<int> AddAsync(Address address)
        {
            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();
            return address.Id;
        }
        public async Task DeleteAsync(Address address)
        {
            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Address address)
        {
            // var existingAddress = await _dbContext.Addresses.FindAsync(address.Id);
            // if (existingAddress != null)
            // {
            //     _dbContext.Entry(existingAddress).State = EntityState.Detached;
            // }
            _dbContext.Entry(address).State = EntityState.Modified;
            _dbContext.Addresses.Update(address);
            await _dbContext.SaveChangesAsync();
        }
    }
}