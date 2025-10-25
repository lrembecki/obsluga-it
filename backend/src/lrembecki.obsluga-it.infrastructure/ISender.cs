using lrembecki.obsluga_it.application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.obsluga_it.infrastructure;

public interface ISender
{
    Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IRequest;
    Task<Y> SendAsync<Y>(IRequest<Y> request, CancellationToken cancellationToken = default);
}

internal class Sender(IServiceProvider serviceProvider) : ISender
{

    public async Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IRequest
    {
        try
        {
            BehaviorHandlerDelegate handlerDelegate = () => serviceProvider.GetRequiredService<IRequestHandler<T>>().HandleAsync(request);
            await BuildBehaviorhandler(GetOrderedBehaviors(), handlerDelegate, cancellationToken);
        }
        catch (Exception ex)
        {
            HandleException(ex, cancellationToken);
            throw;
        }
    }
    public async Task<Y> SendAsync<Y>(IRequest<Y> request, CancellationToken cancellationToken = default)
    {
        try
        {
            Y result = default!;

            var type = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(Y));
            var handler = serviceProvider.GetRequiredService(type);

            BehaviorHandlerDelegate handlerDelegate = async () => result = await (type.GetMethod("HandleAsync")!.Invoke(handler, new object[] { request, cancellationToken }) as Task<Y>)!;
            await BuildBehaviorhandler(GetOrderedBehaviors(), handlerDelegate, cancellationToken);

            return result;
        }
        catch (Exception ex)
        {
            HandleException(ex, cancellationToken);
            throw;
        }
    }

    private static async Task BuildBehaviorhandler(List<IBehavior> behaviorHandlers, BehaviorHandlerDelegate handlerDelegate, CancellationToken cancellationToken)
    {
        var pipeline = new BehaviorPipelineBuilder();

        foreach (var behavior in behaviorHandlers)
        {
            pipeline.Use(next => async () => await behavior.HandleAsync(next, cancellationToken));
        }

        var build = pipeline.Build(handlerDelegate);
        await build();
    }
    private List<IBehavior> GetOrderedBehaviors() =>
        serviceProvider.GetServices<IBehavior>().OrderBy(b => b.Priority).ToList();

    private void HandleException(Exception ex, CancellationToken cancellationToken)
    {
        Parallel.ForEach(serviceProvider.GetServices<IExceptionHandler>(),
            async handler => await handler.HandleAsync(ex, cancellationToken));
    }
}

internal class BehaviorPipelineBuilder
{
    private readonly List<Func<BehaviorHandlerDelegate, BehaviorHandlerDelegate>> _components = [];

    public BehaviorPipelineBuilder Use(Func<BehaviorHandlerDelegate, BehaviorHandlerDelegate> middleware)
    {
        _components.Add(middleware);
        return this;
    }

    public BehaviorHandlerDelegate Build(BehaviorHandlerDelegate finalHandler)
    {
        BehaviorHandlerDelegate app = finalHandler;

        for (int i = _components.Count - 1; i >= 0; i--)
        {
            app = _components[i](app);
        }

        return app;
    }
}

