using Application.Common.Errors;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product
        (
            request.Name,
            request.Description,
            request.Price,
            request.Stock,
            request.CategoryId,
            request.ImageUrl
        );

        var result = await _productRepository.GetByNameAsync(product.Name);
        if (result != null)
        {
            throw new ProductNameExistException();
        }
        var productid = await _productRepository.AddAsync(product);
        product.SetProductId(productid);

        return product;
    }
}