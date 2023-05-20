using Domain.Entities;
using MediatR;

namespace Application.Products.Queries.SearchProducts;
public class SearchProductsQuery : IRequest<IEnumerable<Product>>
{
    public string SearchTerm { get; set; } = null!;
    public string? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}