using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;
public class Order
{
    public Order()
    {
    }
    public Order(int customerId, Customer customer,int shippingAddressId)
    {
        Id = new OrderId(Guid.NewGuid());
        Customer = customer;
        CustomerId = customerId;
        ShippingAddressId = shippingAddressId;
        OrderDate = DateTime.UtcNow;
    }
    public Order(OrderId id, int customerId, int status)
    {
        Id = id;
        CustomerId = customerId;
        Status = status switch
        {
            0 => OrderStatus.Pending,
            1 => OrderStatus.Shipped,
            2 => OrderStatus.Delivered,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid status")
        };
    }

    public OrderId Id { get; private set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; }
    public int CustomerId { get; private set; }
    public int ShippingAddressId { get; set; }
    public Address ShippingAddress { get; set; }
    public Customer Customer { get; set; } = null!;
    public ICollection<ProductOrder> ProductOrders { get; init; } = new List<ProductOrder>();
}