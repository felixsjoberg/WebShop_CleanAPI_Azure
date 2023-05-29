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

        builder.Property(c => c.UserId)
            .HasMaxLength(450)
            .HasColumnType("nvarchar(450)")
            .IsRequired();

        builder.HasOne<ApplicationUser>()
            .WithOne(c => c.Customer)
            .HasForeignKey<Customer>(o => o.UserId);

        // One customer has one address
        builder.HasOne(c => c.Address)
            .WithOne(a => a.Customer)
            .HasForeignKey<Customer>(c => c.AddressId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}