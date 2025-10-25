using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionRepository(ApplicationDbContext dbContext) : ISubscriptionRepository
{
    private readonly DbSet<Subscription> _subscriptions = dbContext.Set<Subscription>();

    public async Task<Subscription> AddAsync(Subscription subscription)
    {
        _subscriptions.Add(subscription);

        await dbContext.SaveChangesAsync();

        return subscription;
    }

    public Task<List<SubscriptionVM>> GetAllAsync()
        => _subscriptions.Select(e => SubscriptionVM.MapFromDomainEntity(e)).ToListAsync();

    public Task<Subscription?> GetByIdAsync(Guid id)
        => _subscriptions.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<Subscription> UpdateAsync(Subscription subscription)
    {
        _subscriptions.Update(subscription);
        await dbContext.SaveChangesAsync();

        return subscription;
    }
}
