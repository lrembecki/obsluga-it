using lrembecki.core.Services;

namespace lrembecki.infrastructure;

internal class Sender(IServiceProvider serviceProvider) : ISender
{
    public Task Send<T>(T command, CancellationToken ct = default) where T : IRequest
    {
        var handlerType = typeof(IHandler<>).MakeGenericType(typeof(T));
        var handler = serviceProvider.GetService(handlerType);
        
        if (handler is null)
        {
            throw new InvalidOperationException($"No handler found for command of type {typeof(T).FullName}");
        }

        return (handler as IHandler<T>)!.Handle((dynamic)command, ct);
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> command, CancellationToken ct = default)
    {
        var type = command.GetType();
        var handlerType = typeof(IHandler<,>).MakeGenericType(type, typeof(TResponse));
        dynamic handler = serviceProvider.GetService(handlerType)!;

        if (handler is null)
        {
            throw new InvalidOperationException($"No handler found for command of type {type.FullName} with response type {typeof(TResponse).FullName}");
        }

        return handler!.Handle((dynamic)command, ct);
    }
}