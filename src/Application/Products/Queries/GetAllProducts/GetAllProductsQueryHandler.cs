using Application.Common.Interfaces.Persistence;
using Application.Products.Response.Queries;
using MediatR;

namespace Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            return new GetAllProductsResult(products);
        }
    }
}