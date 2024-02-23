using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Abstractions;
using Authentication.Abstractions.AccessPolicies;
using Authentication.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.AccessPolicies;

public class Jwt : IJwt
{
    private readonly string jwtKey;

    public Jwt(IOptions<AuthenticationOptions> authOptions)
    {
        jwtKey = authOptions.Value.JwtKey;
    }

    public string GenerateJwtToken(bool isAdmin, string userName)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.Role, isAdmin ? UserRoles.AdminRole : UserRoles.MemberRole)
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