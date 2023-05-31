using Domain.ValueObjects;

namespace Domain.Entities;
public class ProductOrder
{
    public ProductOrder(int quantity, ProductId productId, OrderId orderId)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be a positive value.");
        }
        Quantity = quantity;
        ProductId = productId;
        OrderId = orderId;
    }
    public int Quantity { get; set; } = 0;
    public ProductId ProductId { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public OrderId OrderId { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public decimal CalculateTotalPrice()
    {
        if (Product == null)
        {
            throw new InvalidOperationException("Product is not set.");
        }

        return Product.Price * Quantity;
    }
    public void UpdateQuantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be a positive value.");
        }
        Quantity = quantity;
    }
}
