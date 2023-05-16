namespace Application.Authentication.Common
{
    public record LoginResponse(string Token, DateTime Expiration);
}