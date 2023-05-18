using Domain.ValueObjects;

namespace Domain.Entities;
public class ProductOrder
{
    public ProductId ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public OrderId OrderId { get; set; }
    public Order? Order { get; set; }
}
