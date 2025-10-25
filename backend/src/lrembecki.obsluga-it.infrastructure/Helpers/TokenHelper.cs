using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace lrembecki.obsluga_it.infrastructure.Helpers;

internal class TokenHelper(IConfiguration configuration) : ITokenHelper
{
    public string CreateToken(
        UserVM userVM,
        IEnumerable<string> permissionVMs,
        SubscriptionVM subscriptionVM,
        DateTime created,
        DateTime expires)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        {
            Issuer = configuration["OpenId:Issuer"],
            Audience = configuration["OpenId:Audience"],
            SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(
                new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["OpenId:Secret"]!)),
                Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256),
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
