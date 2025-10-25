using lrembecki.obsluga_it.application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace lrembecki.obsluga_it.application;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        var assembly = typeof(ServiceRegistration).Assembly;

        assembly.GetTypes().ToList().ForEach(type =>
        {
            if (type.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false)) return;

            type.GetInterfaces().Where(e => e.IsGenericType).ToList().ForEach(handlerType =>
            {
                var genericDef = handlerType.GetGenericTypeDefinition();
                if (genericDef == typeof(IRequestHandler<>) || genericDef == typeof(IRequestHandler<,>))
                {
                    services.AddTransient(handlerType, type);
                }
            });
        });

        assembly.GetTypes().ToList()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => !t.IsDefined(typeof(CompilerGeneratedAttribute), inherit: false))
            .Select(t => new { Impl = t, Interface = t.GetInterface($"I{t.Name}") })
            .Where(x => x.Interface != null && x.Interface.Namespace != null && x.Interface.Namespace.StartsWith("lrembecki.obsluga_it."))
            .ToList()
            .ForEach(x => services.AddTransient(x.Interface!, x.Impl));
    }
}
