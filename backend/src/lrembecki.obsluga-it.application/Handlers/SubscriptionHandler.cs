using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Commands;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class SubscriptionHandler(IUnitOfWork uow)
    : IRequestHandler<SubscriptionCreateCommand, SubscriptionVM>
    , IRequestHandler<SubscriptionUpdateCommand, SubscriptionVM>
{
    private readonly IRepository<SubscriptionEntity> _subscriptions = uow.GetRepository<SubscriptionEntity>();

    public async Task<SubscriptionVM> HandleAsync(SubscriptionCreateCommand request, CancellationToken cancellationToken = default)
    {
        var subscription = new domain.Entities.SubscriptionEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _subscriptions.AddAsync(subscription);

        return SubscriptionVM.MapFromDomainEntity(subscription);
    }

    public async Task<SubscriptionVM> HandleAsync(SubscriptionUpdateCommand request, CancellationToken cancellationToken = default)
    {
        var subscription = (await _subscriptions.FindByIdAsync(request.SubscriptionId, cancellationToken))
            ?? throw new Exception("Subscription not found");

        subscription.Name = request.Name;

        await _subscriptions.UpdateAsync(subscription);

        return SubscriptionVM.MapFromDomainEntity(subscription);
    }
}
