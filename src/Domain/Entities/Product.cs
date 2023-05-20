using Domain.ValueObjects;

namespace Domain.Entities;

public class Product
{
    public Product(ProductId id, string name, string description, decimal price, int stock, int categoryId, string imageUrl)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
    }

    public ProductId Id { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public int Stock { get; set; }
    public bool IsActive { get; set; } = true;
    public string ImageUrl { get; set; } = null!;
    public int CategoryId { get; }
    public Category Category { get; init; } = null!;
    public ICollection<ProductOrder> ProductOrders { get; init; } = new List<ProductOrder>();

}