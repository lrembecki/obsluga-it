using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using lrembecki.core.Events;
using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public const string AzureAdScheme = "AzureAd";
    public const string InternalJwtScheme = "InternalJwt";
    public const string AzureAdUserScopePolicy = "AzureAdUserScope";
    public const string InternalJwtPolicy = "InternalJwtPolicy";
    public const string LocalMockupPolicy = "LocalMockupPolicy";

    public static void AddProjectAuthentication(this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = InternalJwtScheme;
                options.DefaultChallengeScheme = InternalJwtScheme;
            })
            .AddJwtBearer(InternalJwtScheme, o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["OpenId:Secret"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuers = [builder.Configuration["OpenId:Issuer"]],
                    ValidAudiences = [builder.Configuration["OpenId:Audience"]],
                };
            })
            .AddMicrosoftIdentityWebApi(
                builder.Configuration.GetSection("AzureAd"),
                jwtBearerScheme: AzureAdScheme);

        builder.Services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(InternalJwtScheme)
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy(AzureAdUserScopePolicy, policy =>
            {
                policy
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(AzureAdScheme);
            })
            .AddPolicy(InternalJwtPolicy, policy =>
            {
                policy
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(InternalJwtScheme); ;
            });
    }
}
