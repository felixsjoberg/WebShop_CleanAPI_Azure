using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
int Id,
string Name,
string Description,
decimal Price,
int Stock,
int CategoryId,
string ImageUrl
) : IRequest<Unit>;