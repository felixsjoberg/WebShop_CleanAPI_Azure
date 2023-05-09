using Application.Common.Interfaces.Persistence;
using BankApplication.Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence.Base;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
        services.AddDbContext<TopStyleDbContext>(options => options.UseSqlServer());

        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}