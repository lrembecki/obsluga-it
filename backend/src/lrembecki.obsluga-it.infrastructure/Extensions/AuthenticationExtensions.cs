using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace lrembecki.obsluga_it.infrastructure;

public static class AuthenticationExtensions
{
    public const string AzureAdScheme = "AzureAd";
    public const string InternalJwtScheme = "InternalJwt";
    public const string AzureAdUserScopePolicy = "AzureAdUserScope";

    public static IServiceCollection AddProjectAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                // Default authenticate can remain AzureAd (for initial sign-in)
                options.DefaultAuthenticateScheme = AzureAdScheme;
                options.DefaultChallengeScheme = AzureAdScheme;
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

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AzureAdUserScopePolicy, policy =>
            {
                policy
                    .AddAuthenticationSchemes(AzureAdScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim("scp", "access_as_user");
            });
        });

        return services;
    }
}
