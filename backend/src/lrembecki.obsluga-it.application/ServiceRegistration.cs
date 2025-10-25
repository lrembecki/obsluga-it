using lrembecki.obsluga_it.application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.obsluga_it.application;

public static class ServiceRegistration
{
    public static void AddServices(this IServiceCollection services)
    {
        typeof(ServiceRegistration).Assembly.GetTypes().ToList().ForEach(type =>
        {
            type.GetInterfaces().Where(e => e.IsGenericType).ToList().ForEach(handlerType =>
            {
                var genericDef = handlerType.GetGenericTypeDefinition();
                if (genericDef == typeof(IRequestHandler<>) || genericDef == typeof(IRequestHandler<,>))
                {
                    services.AddTransient(handlerType, type);
                }
            });
        });

        typeof(ServiceRegistration).Assembly.GetTypes().ToList()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
            .ToList()
            .ForEach(implType =>
            {
                var interfaceType = implType.GetInterface($"I{implType.Name}");
                if (interfaceType != null)
                {
                    services.AddTransient(interfaceType, implType);
                }
            });
    }
}
