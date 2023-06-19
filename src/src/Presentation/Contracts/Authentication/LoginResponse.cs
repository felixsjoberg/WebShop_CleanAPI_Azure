namespace Contracts.Authentication;
public record LoginResponse(string Token, DateTime Expiration);