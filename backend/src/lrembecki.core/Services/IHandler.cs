
namespace lrembecki.core.Services;

public interface IHandler<TRequest>
    where TRequest : IRequest
{
    Task Handle(TRequest command, CancellationToken ct = default);
}

public interface IHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest command, CancellationToken ct = default);
}
