using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Azure.Identity;
using Infrastructure.Authentication;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration, IHostEnvironment environment)
    {
        services
            .AddAuth(configuration)
            .AddDbContext(configuration, environment)
            .AddPersistence();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TopStyleDbContext>()
            .AddDefaultTokenProviders();

        //JWT Bearer authentication scheme as the default scheme for authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);

        services.AddSingleton(Options.Create(JwtSettings));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }

    public static IServiceCollection AddDbContext(
        this IServiceCollection services, ConfigurationManager configuration, IHostEnvironment environment)
    {
        var settings = new DataSettings();
        configuration.Bind(DataSettings.SectionName, settings);
        services.AddSingleton(Options.Create(settings));

        var keyVaultURL = configuration.GetSection("KeyVault:KeyVaultURL");
        var keyVaultClientId = configuration.GetSection("KeyVault:ClientId");
        var keyVaultClientSecret = configuration.GetSection("KeyVault:ClientSecret");
        var keyVaultDirectoryId = configuration.GetSection("KeyVault:DirectoryId");

        var credential = new ClientSecretCredential(keyVaultDirectoryId.Value!, keyVaultClientId.Value!, keyVaultClientSecret.Value!);

        configuration.AddAzureKeyVault(keyVaultURL.Value!, keyVaultClientId.Value!, keyVaultClientSecret.Value!, new DefaultKeyVaultSecretManager());

        var client = new SecretClient(new Uri(keyVaultURL.Value!), credential);
        services.AddDbContext<TopStyleDbContext>(options => options.UseSqlServer(client.GetSecret("ProdConnection").Value.Value.ToString(),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));
        var jwtSecretKey = client.GetSecret("JwtSecretKey").Value.Value.ToString();

        var JwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, JwtSettings);
        services.AddSingleton(Options.Create(JwtSettings));
        JwtSettings.SecretKey = jwtSecretKey;

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtSettings.Issuer,
                ValidAudience = JwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSecretKey))
            });

        // else
        // {
        //     services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        //         .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
        //         {
        //             ValidateIssuer = true,
        //             ValidateAudience = true,
        //             ValidateLifetime = true,
        //             ValidateIssuerSigningKey = true,
        //             ValidIssuer = JwtSettings.Issuer,
        //             ValidAudience = JwtSettings.Audience,
        //             IssuerSigningKey = new SymmetricSecurityKey(
        //                 Encoding.UTF8.GetBytes(JwtSettings.SecretKey))
        //         });
        // }
        return services;
    }
}