using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionRepository(ApplicationDbContext dbContext) : EfRepository<Subscription>(dbContext), ISubscriptionRepository
{
    public Task<List<SubscriptionVM>> GetAllSubscriptionVMAsync(CancellationToken cancellationToken = default)
        => _dbSet.Select(e => SubscriptionVM.MapFromDomainEntity(e)).ToListAsync(cancellationToken);
}
