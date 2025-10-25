namespace lrembecki.obsluga_it.application.Abstractions;

public interface IRequest;
public interface IRequest<T>;

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
