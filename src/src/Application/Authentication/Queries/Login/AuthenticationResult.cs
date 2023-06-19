namespace Application.Authentication.Queries.Login
{
    public record AuthenticationResult(string Token, DateTime Expiration);
}