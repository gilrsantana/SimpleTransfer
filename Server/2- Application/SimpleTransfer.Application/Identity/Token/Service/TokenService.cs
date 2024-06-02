using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleTransfer.Application.Identity.Token.Interface;
using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Application.Identity.Token.Service;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly SymmetricSecurityKey _key = new (
        Encoding.UTF8.GetBytes(configuration["Jwt:key"] 
                               ?? throw new InvalidOperationException("Jwt:key not found")));
    
    public string GenerateToken(User user)
    {
        var claims = GetClaims(user);
        var tokenDescriptor = GetTokenDescriptor(claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private List<Claim> GetClaims(User user) =>
    [
        new Claim(ClaimTypes.Email, user.Email ?? ""), 
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    ];

    private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claims) =>
        new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature),
            Issuer = configuration["Jwt:Issuer"],
        };
}
