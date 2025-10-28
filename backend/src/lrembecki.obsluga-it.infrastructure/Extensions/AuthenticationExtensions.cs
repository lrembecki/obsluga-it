using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace lrembecki.obsluga_it.infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public const string AzureAdScheme = "AzureAd";
    public const string InternalJwtScheme = "InternalJwt";
    public const string AzureAdUserScopePolicy = "AzureAdUserScope";
    public const string InternalJwtPolicy = "InternalJwtPolicy";
    public const string LocalMockupPolicy = "LocalMockupPolicy";

    public static IServiceCollection AddProjectAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        services
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
                        Encoding.UTF8.GetBytes(configuration["OpenId:Secret"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuers = [configuration["OpenId:Issuer"]],
                    ValidAudiences = [configuration["OpenId:Audience"]],
                };
            })
            .AddMicrosoftIdentityWebApi(
                configuration.GetSection("AzureAd"),
                jwtBearerScheme: AzureAdScheme);

        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(InternalJwtScheme)
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy(AzureAdUserScopePolicy, policy =>
            {
                policy
                    .RequireAuthenticatedUser()
                    .RequireClaim("scp", "access_as_user");

                if (!isDevelopment)
                {
                    policy
                        .AddAuthenticationSchemes(AzureAdScheme);
                }
            })
            .AddPolicy(InternalJwtPolicy, policy =>
            {
                policy
                    .RequireAuthenticatedUser();

                if (!isDevelopment)
                {
                    policy
                        .AddAuthenticationSchemes(InternalJwtScheme);
                }
            });

        return services;
    }
}
