
namespace lrembecki.core.Services;

public interface ISender
{
    public Task Send<T>(T command, CancellationToken ct = default)
        where T : IRequest;

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> command, CancellationToken ct = default);
}