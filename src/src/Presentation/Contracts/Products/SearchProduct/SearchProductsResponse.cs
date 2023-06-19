namespace Presentation.Contracts.Products.SearchProduct;
public record SearchProductsResponse(IEnumerable<ProductResponse> Products);

public record ProductResponse(
    int Id,
    string Name,
    decimal Price,
    int CategoryId);
