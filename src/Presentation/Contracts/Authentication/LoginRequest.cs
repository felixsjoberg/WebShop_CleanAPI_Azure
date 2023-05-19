using System.ComponentModel.DataAnnotations;

namespace Contracts.Authentication;

public record LoginRequest()

{
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
}