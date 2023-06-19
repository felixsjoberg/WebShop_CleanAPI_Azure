using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public partial class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => new OrderId(value));

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.HasMany(o => o.ProductOrders)
                .WithOne(po => po.Order)
                .HasForeignKey(po => po.OrderId);

            builder.HasOne(o => o.ShippingAddress)
                .WithMany(a => a.Orders)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
