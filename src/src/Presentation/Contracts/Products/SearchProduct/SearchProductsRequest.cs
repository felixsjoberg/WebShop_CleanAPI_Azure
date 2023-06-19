namespace Presentation.Contracts.Products.SearchProduct;
public record SearchProductsRequest(string SearchTerm,decimal? MinPrice, decimal? MaxPrice, int? CategoryId);