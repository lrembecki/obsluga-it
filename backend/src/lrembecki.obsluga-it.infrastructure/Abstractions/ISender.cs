using lrembecki.shared.application.Abstractions;

namespace lrembecki.obsluga_it.infrastructure.Abstractions;

public interface ISender
{
    Task SendAsync<T>(T request, CancellationToken cancellationToken = default) where T : IRequest;
    Task<Y> SendAsync<Y>(IRequest<Y> request, CancellationToken cancellationToken = default);
}

