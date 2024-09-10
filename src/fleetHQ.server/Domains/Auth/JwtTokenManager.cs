using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using FleetHQ.Server.Configuration;

using Microsoft.IdentityModel.Tokens;

namespace FleetHQ.Server.Domains.Auth;

public interface IJwtTokenManager
{
    string GenerateAccessToken(Guid userId, Guid roleId, Guid? companyId = null);
    string? GetNameIdentifierFromToken(string token);
}

public class JwtTokenManager : IJwtTokenManager
{
    public string GenerateAccessToken(Guid userId, Guid roleId, Guid? companyId)
    {
        var claims = new[]
        {
             new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
             new Claim(ClaimTypes.Role, roleId.ToString()),
             new Claim("CompanyId", companyId?.ToString() ?? string.Empty)
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

    public string? GetNameIdentifierFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        if (handler.CanReadToken(token))
        {
            var jwtToken = handler.ReadJwtToken(token);

            // Extract the 'nameid' claim
            var nameIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return nameIdClaim?.Value;
        }

        return null;
    }




}