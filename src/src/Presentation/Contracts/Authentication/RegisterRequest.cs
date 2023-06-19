using System.ComponentModel.DataAnnotations;

namespace Contracts.Authentication;

public record RegisterRequest(
    [Required(ErrorMessage = "UserName is required")]
    string Username,

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    string Password,

    [Required]
    RegisterCustomerRequest Customer
);
public record RegisterCustomerRequest(
    string FullName,
    string Streetaddress,
    string City,
    string Zipcode,
    string Country);