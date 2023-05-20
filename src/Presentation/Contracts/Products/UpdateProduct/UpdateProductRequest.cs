namespace Presentation.Contracts.Products.UpdateProduct;

public record UpdateProductRequest(int Id, string Name, string Description, decimal Price, int Stock, int CategoryId);