using Domain.Enums;
namespace Domain.Entities;
public class Customer
{
    public Customer()
    {
    }
    public Customer(string fullName, string streetaddress, string city, string zipcode, string country, string countryCode, string userId)
    {
        FullName = fullName;
        Streetaddress = streetaddress;
        City = city;
        Zipcode = zipcode;
        Country = country;
        CountryCode = countryCode;
        UserId = userId;
    }
    public Customer(string fullName, string streetaddress, string city, string zipcode, string country, string countryCode)
    {
        FullName = fullName;
        Streetaddress = streetaddress;
        City = city;
        Zipcode = zipcode;
        Country = country;
        CountryCode = countryCode;
    }
    public int Id { get; set; }
    public string FullName { get; private set; } = null!;

    public string Streetaddress { get; private set; } = null!;

    public string City { get; private set; } = null!;

    public string Zipcode { get; private set; } = null!;

    public string Country { get; private set; } = null!;

    public string CountryCode { get; private set; } = null!;

    public string? UserId { get; set; }
    public ICollection<Order> Orders { get; init; } = new List<Order>();

    public void UpdateCustomer(string fullName, string streetaddress, string city, string zipcode, string country, string countryCode)
    {
        FullName = fullName;
        Streetaddress = streetaddress;
        City = city;
        Zipcode = zipcode;
        Country = country;
        CountryCode = countryCode;
    }
}