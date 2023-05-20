using Domain.Entities;

namespace Application.Products.Response.Queries;

public record GetAllProductsResult(IEnumerable<Product> Products);
