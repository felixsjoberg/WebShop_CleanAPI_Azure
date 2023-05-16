namespace Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    JwtToken GenerateToken(string UserName);
}