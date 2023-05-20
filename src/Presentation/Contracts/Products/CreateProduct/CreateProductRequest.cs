using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Products.CreateProduct;
public record CreateProductRequest(
string Name,
string Description,
[Range(0.01, 500000, ErrorMessage = "Price must be greater than 0")]
decimal Price,
string ImageUrl,
int CategoryId,
int Stock = 1);