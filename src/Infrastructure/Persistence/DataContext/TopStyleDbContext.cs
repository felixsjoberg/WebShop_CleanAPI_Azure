using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataContext;

public class TopStyleDbContext : IdentityDbContext<ApplicationUser>
{
    public TopStyleDbContext(DbContextOptions<TopStyleDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}