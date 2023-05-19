namespace Application.Authentication.Common
{
    public record AuthenticationResult(string Token, DateTime Expiration);
}