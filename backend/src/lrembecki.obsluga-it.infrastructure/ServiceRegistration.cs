using Azure.Core;
using Azure.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.obsluga_it.infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configuration,
        bool isDevelopment)
    {
        configuration.AddAzureAppConfiguration(ac =>
        {
            TokenCredential credential = isDevelopment
                ? new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    TenantId = configuration["EntraId:TenantId"]
                })
                : new ManagedIdentityCredential(clientId: configuration["ClientId"]);

            ac.ConfigureKeyVault(kv => kv.SetCredential(credential));
            ac.Connect(new Uri(configuration.GetConnectionString("AppConfiguration")!), credential);
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sql")));

        services.AddScoped<ApplicationDbContext>();

        typeof(ServiceRegistration).Assembly.GetTypes().ToList()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
            .ToList()
            .ForEach(implType =>
            {
                var interfaceType = implType.GetInterface($"I{implType.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, implType);
                }
            });
    }
}