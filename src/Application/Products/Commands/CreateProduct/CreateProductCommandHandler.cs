using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var product = new Product
        // (
        //     ProductId = new ProductId(1),
        //     request.Name,
        //     request.Description,
        //     request.Price,
        //     request.Stock,
        //     request.CategoryId,
        //     request.ImageUrl
        // );

        // var result = await _productRepository.AddAsync(product);

        // if (result is not Product productDetails)
        // {
        //     throw new InternalServerError();
        // }

        // return new CreateProductResult(productDetails);
    }
}