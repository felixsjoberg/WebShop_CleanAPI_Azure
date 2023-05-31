using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    IEnumerable<OrderItem> OrderItems,
    OrderDetails OrderDetails
) : IRequest<CreateOrderResult>;

public record OrderItem(
    int ProductId,
    int Quantity
);

public record OrderDetails(
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country);
