using Azure.Core;
using Azure.Identity;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.infrastructure.Extensions;
using lrembecki.obsluga_it.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

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

        services.AddProjectAuthentication(configuration);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sql"), _ =>
            {
                //_.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                _.MigrationsHistoryTable("__EFMigrationsHistory", "oit");
            }));

        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        typeof(ServiceRegistration).Assembly.GetTypes().ToList()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => !t.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false))
            .Select(t => new { Impl = t, Interface = t.GetInterface($"I{t.Name}") })
            .Where(x => x.Interface != null && x.Interface.Namespace != null && x.Interface.Namespace.StartsWith("lrembecki.obsluga_it."))
            .ToList()
            .ForEach(x => services.AddScoped(x.Interface!, x.Impl));
    }
}