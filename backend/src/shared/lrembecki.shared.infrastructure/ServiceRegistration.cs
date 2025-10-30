using lrembecki.obsluga_it.infrastructure.Providers;
using lrembecki.shared.application.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.shared.infrastructure;

public static class ServiceRegistration
{
    public static void AddSharedInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configuration,
        bool isDevelopment)
    {

        services.AddScoped<IDateProvider, DateProvider>();
    }
}