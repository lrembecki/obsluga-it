using lrembecki.core.Helpers;
using lrembecki.infrastructure.Defaults;
using lrembecki.infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace lrembecki.infrastructure.Helpers;

internal class JwtTokenFactory(IConfiguration configuration) : IJwtTokenFactory
{
    public string CreateToken(ClaimsIdentity identity, IEnumerable<string> permissionVMs, DateTime created, DateTime expires)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
#pragma warning disable S6781 // JWT secret keys should not be disclosed
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["OpenId:Secret"]!));
#pragma warning restore S6781 // JWT secret keys should not be disclosed

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = configuration["OpenId:Issuer"],
            Audience = configuration["OpenId:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
            Subject = identity,
            Expires = expires,
            IssuedAt = created
        });

        return tokenHandler.WriteToken(token);
    }

    public ClaimsIdentity GetClaimsIdentity(Guid subscriptionId, Guid userId, string email, string[] permissions, DateTime created, DateTime expires)
        => new ClaimsIdentity([
                new Claim(CustomClaimTypes.Subject, userId.ToString()),
                new Claim(CustomClaimTypes.UserId, userId.ToString()),
                new Claim(CustomClaimTypes.SubscriptionId, subscriptionId.ToString()),
                new Claim(CustomClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(CustomClaimTypes.Email, email),
                ..permissions.Select(p => new Claim(CustomClaimTypes.Role, p)),
                new Claim(CustomClaimTypes.Created, created.ToString()),
                new Claim(CustomClaimTypes.Expires, expires.ToString())
            ], AuthenticationExtensions.InternalJwtScheme);
}
