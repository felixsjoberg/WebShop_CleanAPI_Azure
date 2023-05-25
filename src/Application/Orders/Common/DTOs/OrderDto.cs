using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Orders.Common.DTOs;

public record OrderDto(
Guid Id,
string Status,
DateTime OrderDate,
CustomerDetailsDto Customer,
List<ProductsOnOrderDto> Products
);

public record CustomerDetailsDto(
int Id,
string FullName,
string Streetaddress,
string City,
string Zipcode,
string Country
);

public record ProductsOnOrderDto(
int ProductId,
string Name,
decimal Price,
int Quantity
);