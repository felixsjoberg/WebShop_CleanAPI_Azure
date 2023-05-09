using Application.Common.Interfaces.Persistence;
using Application.Products.Queries;
using Application.Products.Response.Queries;
using MediatR;

namespace Application.Authentication.Queries.Login;

public class GetProductQueryHandler :
    IRequestHandler<GetProductQuery, GetProductResponse>
{

    private readonly IProductRepository _productRepository;


    public GetProductQueryHandler(IProductRepository productRepository)
    {
            _productRepository = productRepository;
    }

    public async Task<GetProductResponse> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductAsync(query.Id);

        if (product is null)
        {
            throw new Exception("User does not exist");
        }

        return new GetProductResponse(product
            );
    }
}
