using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Commands;

public record SubscriptionCreateCommand(string Name): IRequest<SubscriptionVM>;
public record SubscriptionUpdateCommand(Guid SubscriptionId, string Name): IRequest<SubscriptionVM>;

internal sealed class SubscriptionCommandHandler(IUnitOfWork uow)
    : IRequestHandler<SubscriptionCreateCommand, SubscriptionVM>
    , IRequestHandler<SubscriptionUpdateCommand, SubscriptionVM>
{
    private readonly ISubscriptionRepository _subscriptions = uow.GetRepository<ISubscriptionRepository>();

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
