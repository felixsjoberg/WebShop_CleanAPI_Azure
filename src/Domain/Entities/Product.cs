using Domain.ValueObjects;

namespace Domain.Entities;

public class Product
{
    public Product()
    {
    }
    public Product(string name, string description, decimal price, int stock, int categoryId, string imageUrl)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price must be a positive value.");
        }
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
    }
    public Product(ProductId id, string name, string description, decimal price, int stock, int categoryId, bool isActive, string imageUrl)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price must be a positive value.");
        }
        if (categoryId < 0)
        {
            throw new ArgumentException("CategoryId must be a positive value.");
        }
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        IsActive = isActive;
        ImageUrl = imageUrl;
    }
    public Product(ProductId id, string name, string description, decimal price, int stock, int categoryId, string imageUrl)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price must be a positive value.");
        }
        if (categoryId < 0)
        {
            throw new ArgumentException("CategoryId must be a positive value.");
        }
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
    }

    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; protected set; }
    public bool IsActive { get; set; } = true;
    public string ImageUrl { get; set; } = null!;
    public int CategoryId { get; private set; }
    public Category Category { get; init; } = null!;
    public ICollection<ProductOrder> ProductOrders { get; init; } = new List<ProductOrder>();

    public void Update(string name, string description, decimal price, int stock, string imageUrl, int categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        ImageUrl = imageUrl;
        CategoryId = categoryId;
    }
    public void DecreaseStock(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity must be a positive value.");
        }

        if (Stock < quantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }

        Stock -= quantity;
    }
    public void SetProductId(int id)
    {
        Id = new ProductId(id);
    }
    public Product Create(
            ProductId id,
            string name,
            string description,
            decimal price,
            int stock,
            string imageUrl,
            int categoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        ImageUrl = imageUrl;
        return this;
    }
}