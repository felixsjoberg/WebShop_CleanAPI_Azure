namespace Application.Products.Queries.SearchProducts;

public record SearchProductsResult(
    string Id,
    string Name,
    string Description,
    decimal Price,
    string Category,
    string ImageUrl,
    int Stock
);