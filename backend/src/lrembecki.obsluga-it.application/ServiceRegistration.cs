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
    }
}
