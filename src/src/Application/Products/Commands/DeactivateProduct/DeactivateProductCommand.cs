using MediatR;

namespace Application.Products.Commands.DeactivateProduct;

public record DeactivateProductCommand(int id): IRequest<Unit>;