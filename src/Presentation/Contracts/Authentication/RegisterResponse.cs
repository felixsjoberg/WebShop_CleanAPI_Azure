namespace Contracts.Authentication;
public record AuthenticationResponse(string Token, DateTime Expiration);