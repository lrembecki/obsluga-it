using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionUserRepository(ApplicationDbContext dbcontext, ISessionAccessor sessionAccessor) : EfRepository<SubscriptionUser>(dbcontext), ISubscriptionUserRepository
{
    public override IQueryable<SubscriptionUser> GetAll()
    {
        return base.GetAll().Where(e => e.SubscriptionId == sessionAccessor.SubscriptionId);
    }

    public Task<SubscriptionUser?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId)
        => GetAll()
            .Include(e => e.Subscription)
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.User.Email.Address == email && (subscriptionId == null || e.SubscriptionId == subscriptionId));
}