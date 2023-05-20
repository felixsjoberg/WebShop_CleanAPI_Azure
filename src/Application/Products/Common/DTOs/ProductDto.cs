using Domain.ValueObjects;
namespace Application.Products.Common.DTOs;

public record ProductDto
(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    int CategoryId,
    string ImageUrl
);

public record ProductDtoList(IEnumerable<ProductDto> Products);