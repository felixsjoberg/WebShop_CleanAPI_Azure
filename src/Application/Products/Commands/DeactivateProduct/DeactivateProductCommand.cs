using Domain.ValueObjects;
using MediatR;

namespace Application.Products.Commands.DeactivateProduct;

public record DeactivateProductCommand(ProductId Id): IRequest<DeactivateProductResult>;