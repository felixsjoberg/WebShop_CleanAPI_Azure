using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<int> AddAsync(Category category);
    Task DeleteAsync(Category category);
}
