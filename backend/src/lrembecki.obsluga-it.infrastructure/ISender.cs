using lrembecki.obsluga_it.application.Abstractions;

namespace lrembecki.obsluga_it.infrastructure;

public interface ISender
{
    Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IRequest;
    Task<Y> SendAsync<Y>(IRequest<Y> request, CancellationToken cancellationToken = default);
}

