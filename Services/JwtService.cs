namespace TastyKitchens.API.Services;

using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TastyKitchens.API.Models;

public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username ?? ""),
            new Claim(ClaimTypes.Role, user.Role ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? "")
        };

        var keyString = _config["Jwt:Key"] ?? "THIS_IS_A_SUPER_SECRET_KEY_1234567890123456";

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(keyString)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"] ?? "test",
            audience: _config["Jwt:Audience"] ?? "test",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}