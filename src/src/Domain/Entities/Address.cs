namespace Domain.Entities;
public class Address
{
    public Address(
        string streetaddress,
        string city,
        string zipcode,
        string country)
    {
        Streetaddress = streetaddress;
        City = city;
        Zipcode = zipcode;
        Country = country;
    }
    public Address(
        int id,
        string streetaddress,
        string city,
        string zipcode,
        string country)
    {
        Id = id;
        Streetaddress = streetaddress;
        City = city;
        Zipcode = zipcode;
        Country = country;
    }

    public int Id { get; set; }
    public string Streetaddress { get; private set; } = null!;

    public string City { get; private set; } = null!;

    public string Zipcode { get; private set; } = null!;

    public string Country { get; private set; } = null!;
    public ICollection<Order> Orders { get; init; } = new List<Order>();
    public Customer? Customer { get; set; }
}