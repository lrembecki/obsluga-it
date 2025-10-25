using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionRepository(ApplicationDbContext dbcontext) : ISubscriptionRepository
{
    private readonly DbSet<Subscription> _subscriptions = dbcontext.Set<Subscription>();
    public Task<List<SubscriptionVM>> GetAllAsync()
        => _subscriptions.Select(e => SubscriptionVM.MapFromDomainEntity(e)).ToListAsync();

    public Task<Subscription?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
