using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Queries;

public record SubscriptionsGetQuery() : IRequest<List<SubscriptionVM>>;
internal sealed class SubscriptionsGetQueryHandler(IUnitOfWork uow) : IRequestHandler<SubscriptionsGetQuery, List<SubscriptionVM>>
{
    public Task<List<SubscriptionVM>> HandleAsync(SubscriptionsGetQuery request, CancellationToken cancellationToken = default)
        => uow.GetRepository<ISubscriptionRepository>().GetAllSubscriptionVMAsync(cancellationToken);
}