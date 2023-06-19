namespace Application.Common;
public class JwtToken
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
}