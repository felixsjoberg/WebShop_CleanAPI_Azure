namespace Application.Authentication.Commands.Register
{
    public record RegsiterResult(string Token, DateTime Expiration, int CustomerId, int AddressId);
}