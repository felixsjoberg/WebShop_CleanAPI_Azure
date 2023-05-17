using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Order
{
    public Order(OrderId id, string userId, OrderStatus status)
    {
        Id = id;
        UserId = userId;
        Status = status;
        OrderDate = DateTime.UtcNow;
    }

    public OrderId Id { get; }
    public string UserId { get; }
    public OrderStatus Status { get; }
    public DateTime OrderDate { get; }
    public ICollection<ProductOrder> ProductOrders { get; init; } = new List<ProductOrder>();
}
