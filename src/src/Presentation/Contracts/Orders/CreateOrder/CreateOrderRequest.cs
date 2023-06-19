namespace Presentation.Contracts.Orders.CreateOrder;
public record CreateOrderRequest(
    IEnumerable<OrderItemResponse> OrderItems,
    CustomerResponse Customer
);

public record OrderItemResponse(
    int ProductId,
    int Quantity
);

public record CustomerResponse(
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country
);