namespace Domain.Entities;
public class Customer
{
    public Customer()
    {
    }
    public Customer(int id,string fullName, string userId, int addressId)
    {
        Id = id;
        FullName = fullName;
        UserId = userId;
        AddressId = addressId;
    }
    public Customer(string fullName, string userId, int addressId)
    {
        FullName = fullName;
        UserId = userId;
        AddressId = addressId;
    }
    public int Id { get; set; }
    public string FullName { get; private set; } = null!;

    public int AddressId { get; set; }
    public Address Address { get; set; }

    public string? UserId { get; set; }
    public ICollection<Order> Orders { get; init; } = new List<Order>();

    public void UpdateCustomer(string fullName)
    {
        FullName = fullName;
    }
}