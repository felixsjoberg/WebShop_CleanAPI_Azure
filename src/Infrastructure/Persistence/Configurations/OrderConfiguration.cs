using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever();

            builder.Property(o => o.UserId)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.HasMany(o => o.ProductOrders)
                .WithOne(po => po.Order)
                .HasForeignKey(po => po.OrderId);
        }
    }
}
