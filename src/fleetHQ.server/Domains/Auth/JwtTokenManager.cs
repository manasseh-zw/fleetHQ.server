using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using FleetHQ.Server.Configuration;

using Microsoft.IdentityModel.Tokens;

namespace FleetHQ.Server.Domains.Auth;

public interface IJwtTokenManager
{
    string GenerateAccessToken(Guid userId, Guid roleId);

}

public class JwtTokenManager : IJwtTokenManager
{
    public string GenerateAccessToken(Guid userId, Guid roleId)
    {
        var claims = new[]
        {
             new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
             new Claim(ClaimTypes.Role, roleId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Appsettings.JwtOptions.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: Appsettings.JwtOptions.Issuer,
            audience: Appsettings.JwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(1),
            signingCredentials: credentials
        );

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;
    }
}