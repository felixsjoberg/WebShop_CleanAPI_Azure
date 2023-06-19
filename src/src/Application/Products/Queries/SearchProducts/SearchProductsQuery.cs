using MediatR;

namespace Application.Products.Queries.SearchProducts;
public class SearchProductsQuery : IRequest<SearchProductsResult>
{
    public string SearchTerm { get; set; } = null!;
    public int? CategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}