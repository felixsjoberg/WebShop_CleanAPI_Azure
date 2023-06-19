using MediatR;

namespace Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(
Guid Id,
int Status,
IEnumerable<UpdateOrderItem> OrderItems,
UpdateOrderCustomerDto Customer
) : IRequest<Unit>;

public record UpdateOrderItem(
    int ProductId,
    int Quantity
);

public record UpdateOrderCustomerDto(
    int Id,
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country
);
