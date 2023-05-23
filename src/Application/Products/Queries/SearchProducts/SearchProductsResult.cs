using Domain.Entities;

namespace Application.Products.Queries.SearchProducts;

public record SearchProductsResult(
    IEnumerable<Product> Products
);