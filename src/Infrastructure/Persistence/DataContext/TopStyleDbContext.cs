using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataContext;

public class TopStyleDbContext : DbContext
{
    public TopStyleDbContext(DbContextOptions<TopStyleDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}