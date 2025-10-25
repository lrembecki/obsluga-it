using lrembecki.obsluga_it.application.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionUserRepository(ApplicationDbContext dbcontext) : ISubscriptionUserRepository
{
    private readonly DbSet<SubscriptionUser> _subscriptionUsers = dbcontext.Set<SubscriptionUser>();

    public Task<SubscriptionUser?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId)
        => _subscriptionUsers
            .Include(e => e.Subscription)
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.User.Email.Address == email && (subscriptionId == null || e.SubscriptionId == subscriptionId));
}