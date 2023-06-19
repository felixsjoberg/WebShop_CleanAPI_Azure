using Application.Common.Interfaces.Persistence;
using Application.Products.Queries;
using Application.Products.Queries.GetProduct;
using MediatR;

namespace Application.Authentication.Queries.Login;

public class GetProductQueryHandler :
    IRequestHandler<GetProductQuery, GetProductResult>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
            _productRepository = productRepository;
    }

    public async Task<GetProductResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(query.Id);

        if (product is null)
        {
            throw new Exception("Product does not exist");
        }

        return new GetProductResult(product);
    }
}
