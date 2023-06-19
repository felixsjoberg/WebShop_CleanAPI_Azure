using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.ToTable("ProductOrders");

            builder.HasKey(po => new { po.ProductId, po.OrderId });

            builder.Property(po => po.Quantity)
                .IsRequired();

            builder.Property(po => po.ProductId)
                .HasConversion(p => p.Value, value => new ProductId(value));

            builder.Property(po => po.OrderId)
                .HasConversion(o => o.Value, value => new OrderId(value));

            builder.HasOne(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductId);

            builder.HasOne(po => po.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.OrderId);
        }
    }
}