using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Extensions;

namespace lrembecki.obsluga_it.application.Services;

public interface ISubscriptionService : ICrudService<SubscriptionDto, SubscriptionVM>;
internal sealed class SubscriptionService(IUnitOfWork uow) : ISubscriptionService
{
    private readonly IRepository<SubscriptionEntity> _subscriptions = uow.GetRepository<SubscriptionEntity>();

    public async Task<SubscriptionVM> CreateAsync(SubscriptionDto model, CancellationToken cancellationToken = default)
    {
        var subscription = SubscriptionEntity.Create(Guid.NewGuid(), model.Name);
        await _subscriptions.AddAsync(subscription);
        return await GetByIdAsync(subscription.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptions.RequireByIdAsync(id, cancellationToken: cancellationToken);
        await _subscriptions.DeleteAsync(subscription);
    }

    public Task<List<SubscriptionVM>> GetAllAsync(CancellationToken cancellationToken = default)
    => _subscriptions.SelectAsync(e => SubscriptionVM.MapFromDomainEntity(e));

    public async Task<SubscriptionVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => SubscriptionVM.MapFromDomainEntity(await _subscriptions.RequireByIdAsync(id, cancellationToken: cancellationToken));

    public async Task<SubscriptionVM> UpdateAsync(Guid id, SubscriptionDto model, CancellationToken cancellationToken = default)
    {
        var subscription = await _subscriptions.RequireByIdAsync(id, cancellationToken: cancellationToken);
        subscription.Name = model.Name; // setter is public
        await _subscriptions.UpdateAsync(subscription);
        return await GetByIdAsync(subscription.Id, cancellationToken);
    }
}
