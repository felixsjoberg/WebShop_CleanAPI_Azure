namespace Application.Authentication.Commands.Register;
public record RegisterResponse(string token, DateTime expiration);