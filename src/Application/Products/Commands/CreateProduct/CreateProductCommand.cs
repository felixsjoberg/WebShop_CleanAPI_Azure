using MediatR;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string ImageUrl,
    int CategoryId) : IRequest<CreateProductResult>;