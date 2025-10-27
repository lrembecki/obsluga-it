using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.infrastructure.Defaults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace lrembecki.obsluga_it.infrastructure.Factories;

internal class JwtTokenFactory(IConfiguration configuration) : IJwtTokenFactory
{
    public string CreateToken(
        UserVM userVM,
        IEnumerable<string> permissionVMs,
        SubscriptionVM subscriptionVM,
        DateTime created,
        DateTime expires)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["OpenId:Secret"]!));

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = configuration["OpenId:Issuer"],
            Audience = configuration["OpenId:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity([
                new Claim(CustomClaimTypes.Subject, userVM.Id.ToString()),
                new Claim(CustomClaimTypes.UserId, userVM.Id.ToString()),
                new Claim(CustomClaimTypes.SubscriptionId, subscriptionVM.Id.ToString()),
                new Claim(CustomClaimTypes.NameIdentifier, userVM.Id.ToString()),
                new Claim(CustomClaimTypes.Email, userVM.Email),
                ..permissionVMs.Select(p => new Claim(CustomClaimTypes.Role, p)),
                new Claim(CustomClaimTypes.Created, created.ToString()),
                new Claim(CustomClaimTypes.Expires, expires.ToString())
            ]),
            Expires = expires,
            IssuedAt = created
        });

        return tokenHandler.WriteToken(token);
    }
}
