using MediatR;

namespace Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id): IRequest<Unit>;
