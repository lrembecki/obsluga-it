using lrembecki.shared.application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace lrembecki.shared.infrastructure;

public static class ServiceRegistration
{
    public static void AddSharedInfrastructureServices(
        this IServiceCollection services,
        ConfigurationManager configuration,
        bool isDevelopment)
    {
        services.AddSharedServices();

        typeof(ServiceRegistration).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => !t.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false))
            .Select(t => new { Impl = t, Interface = t.GetInterface($"I{t.Name}") })
            .Where(x => x.Interface != null && x.Interface.Namespace != null && x.Interface.Namespace.StartsWith("lrembecki.shared."))
            .ToList()
            .ForEach(x => services.AddScoped(x.Interface!, x.Impl));
    }
}