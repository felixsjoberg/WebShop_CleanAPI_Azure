using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand() : IRequest<UpdateProductResult>;