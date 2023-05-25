using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;
public class CategoryRepository : ICategoryRepository
{
    private readonly TopStyleDbContext _dbContext;
    public CategoryRepository(TopStyleDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> AddAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return category.Id;
    }
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var result = await _dbContext.Categories
        .AsNoTracking()
        .Select(x => new Category(
            x.Id,
            x.Name
        ))
        .ToListAsync();

        return result;
    }
}