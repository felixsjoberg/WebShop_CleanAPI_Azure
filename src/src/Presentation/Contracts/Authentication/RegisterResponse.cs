namespace Contracts.Authentication;
public record RegisterResponse(string Token, DateTime Expiration, int CustomerId, int AddressId);