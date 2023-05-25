using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public partial class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.FullName)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.Streetaddress)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(c => c.City)
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(c => c.Zipcode)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(c => c.Country)
            .HasMaxLength(60)
            .HasColumnType("nvarchar(60)")
            .IsRequired();

        builder.Property(c => c.CountryCode)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(c => c.UserId)
            .HasMaxLength(450)
            .HasColumnType("nvarchar(450)")
            .IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithOne(c => c.Customer)
            .HasForeignKey<Customer>(o => o.UserId);

        builder.HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId);
    }
}