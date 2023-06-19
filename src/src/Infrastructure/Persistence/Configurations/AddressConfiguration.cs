using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

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

            builder.HasMany(a => a.Orders)
                .WithOne(o => o.ShippingAddress)
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}