namespace Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    Task<JwtToken> GenerateTokenAsync(string UserName);
}