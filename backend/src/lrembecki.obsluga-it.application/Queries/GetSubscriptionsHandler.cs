using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Repositories;

namespace lrembecki.obsluga_it.application.Queries;

internal sealed class GetSubscriptionsHandler(ISubscriptionRepository repository) : IRequestHandler<GetSubscriptions, List<SubscriptionVM>>
{
    public Task<List<SubscriptionVM>> HandleAsync(GetSubscriptions request, CancellationToken cancellationToken = default)
        => repository.GetAllAsync();
}
