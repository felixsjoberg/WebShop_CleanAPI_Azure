using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Products.Queries.SearchProducts;

public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, SearchProductsResult>
{
    private readonly IProductRepository _productRepository;

    public SearchProductsQueryHandler(IProductRepository productRepository)
    {

        _productRepository = productRepository;
    }

    public async Task<SearchProductsResult> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var query = await _productRepository.SearchAsync(request.SearchTerm, request.CategoryId, request.MinPrice, request.MaxPrice);

        return new SearchProductsResult(query);
    }
}