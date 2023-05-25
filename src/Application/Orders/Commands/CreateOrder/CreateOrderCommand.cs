using MediatR;

namespace Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid UserId,
    IEnumerable<OrderItem> OrderItems,
    CustomerDto Customer
) : IRequest<CreateOrderResult>;

public record OrderItem(
    int ProductId,
    int Quantity
);

public record CustomerDto(
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country,
    string CountryCode
);
