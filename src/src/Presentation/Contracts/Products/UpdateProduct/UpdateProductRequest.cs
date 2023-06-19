using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Products.UpdateProduct;

public record UpdateProductRequest(
int Id,
string Name,
string Description,
[Range(0.01, 500000, ErrorMessage = "Price must be greater than 0")]
decimal Price,
int Stock,
[Range(1, 500000, ErrorMessage = "CategoryId must be greater than 0")]
int CategoryId,
string ImageUrl);