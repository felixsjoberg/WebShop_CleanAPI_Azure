using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions, UserManager<ApplicationUser> userManager)
    {
        _jwtSettings = jwtOptions.Value;
        _userManager = userManager;
    }

    public async Task<JwtToken> GenerateTokenAsync(string UserName)
    {
        if (_jwtSettings.SecretKey == null)
        {
            throw new EmptySecretKeyException();
        }
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        //Register user roles in claims
        var user = await _userManager.FindByNameAsync(UserName);
        var userRoles = await _userManager.GetRolesAsync(user!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, UserName),
            new Claim(JwtRegisteredClaimNames.Sub, user!.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes == 0 ? 60 : _jwtSettings.ExpiryMinutes),
            signingCredentials: signingCredentials);

        return new JwtToken { Token = new JwtSecurityTokenHandler().WriteToken(securityToken), Expiration = securityToken.ValidTo.ToLocalTime() };
    }
}