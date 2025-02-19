using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SharingPlatform.Application.Abstractions;
using SharingPlatform.Application.Settings;

namespace SharingPlatform.Application.Factories;

public sealed class TokenFactory(JwtSettings settings) : ITokenFactory
{
    public string GenerateToken(
        string userId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: credentials);

        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(securityToken);
        
        return token;
    }
}