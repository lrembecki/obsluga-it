using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Queries;

internal sealed class GetSubscriptionsHandler(IUnitOfWork uow) : IRequestHandler<GetSubscriptions, List<SubscriptionVM>>
{
    public Task<List<SubscriptionVM>> HandleAsync(GetSubscriptions request, CancellationToken cancellationToken = default)
        => uow.GetRepository<ISubscriptionRepository>().GetAllAsync();
}
