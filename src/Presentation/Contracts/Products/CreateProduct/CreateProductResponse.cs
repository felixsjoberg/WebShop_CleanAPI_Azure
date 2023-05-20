namespace Presentation.Contracts.Products.CreateProduct;
public record CreateProductResponse(int Id, string Name, string Description, decimal Price, int Stock, int CategoryId);