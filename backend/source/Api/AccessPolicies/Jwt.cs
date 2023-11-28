using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.AccessPolicies;

public interface IJwt
{
    string GenerateJwtToken(ApplicationUser user);
}

public class Jwt : IJwt
{
    private readonly IConfiguration configuration;

    public Jwt(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string GenerateJwtToken(ApplicationUser user)
    {
        if (user is null) throw new Exception("Failed to find the user");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Jwt()));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, user.IsAdmin ? UserRoles.AdminRole : UserRoles.MemberRole)
        };

        var token = new JwtSecurityToken(
            issuer: null, // how do I know if I need an issuer?
            audience: null, // same
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}