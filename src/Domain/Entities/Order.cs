using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Order
{
    public Order()
    {
    }
    public Order(int customerId)
    {
        Id = new OrderId(Guid.NewGuid());
        CustomerId = customerId;
        OrderDate = DateTime.UtcNow;
    }
    public Order(OrderId id, int customerId, OrderStatus status)
    {
        Id = id;
        CustomerId = customerId;
        Status = status;
    }

    public OrderId Id { get; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; }
    public int CustomerId { get; private set; }
    public Customer Customer { get; set; } = null!;
    public ICollection<ProductOrder> ProductOrders { get; init; } = new List<ProductOrder>();
}