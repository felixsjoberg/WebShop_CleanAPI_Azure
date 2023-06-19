using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Products.Commands.DeactivateProduct;

public class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;

    public DeactivateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.id) ?? throw new NotFoundException();
        if (!product.IsActive)
        {
            throw new ProductAlreadyDeactivated();
        }
        product.IsActive = false;

        await _productRepository.DeactivateAsync(product);
        return Unit.Value;
    }
}