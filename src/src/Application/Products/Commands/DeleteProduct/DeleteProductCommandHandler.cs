using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null || product.ProductOrders.Count > 0)
        {
            throw new ProductOnOrdersException();
        }
        await _productRepository.DeleteAsync(product);
        return Unit.Value;
    }
}
