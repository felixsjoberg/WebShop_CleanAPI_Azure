namespace Domain.Entities;
public class Category
{
    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }
    public ICollection<Product> Products { get; init; } = new List<Product>();
}