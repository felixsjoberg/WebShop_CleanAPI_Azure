using Application.Orders.Common.DTOs;

public record UserOrdersDto(
Guid Id,
string Status,
DateTime OrderDate,
List<ProductsOnOrderDto> Products
);