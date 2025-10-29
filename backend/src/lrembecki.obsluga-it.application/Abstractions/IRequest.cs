namespace lrembecki.obsluga_it.application.Abstractions;

public interface IRequest;
#pragma warning disable S2326 // Unused type parameters should be removed
public interface IRequest<T>;
#pragma warning restore S2326 // Unused type parameters should be removed

public interface IBehavior
{
    int Priority { get; }
    Task HandleAsync(BehaviorHandlerDelegate next, CancellationToken cancellationToken = default);
}

public delegate Task BehaviorHandlerDelegate();

public interface IExceptionHandler
{
    Task HandleAsync(Exception exception, CancellationToken cancellationToken = default);
}
