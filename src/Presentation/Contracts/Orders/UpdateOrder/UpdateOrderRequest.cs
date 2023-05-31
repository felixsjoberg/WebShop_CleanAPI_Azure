using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Presentation.Contracts.Orders.UpdateOrder;

public record UpdateOrderRequest(
[Required]
Guid Id,
[RangeAttribute(0, 2)]
OrderStatus Status,
IEnumerable<UpdateOrderItem> OrderItems,
UpdateOrderCustomerDto Customer
);

public record UpdateOrderItem(
    [Required]
    int ProductId,
    [Required]
    [RangeAttribute(1, 100000)]
    int Quantity
);

public record UpdateOrderCustomerDto(
    [Required]
    int Id,
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country
);