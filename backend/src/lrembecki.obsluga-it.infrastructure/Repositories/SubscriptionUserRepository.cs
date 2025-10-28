using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionUserRepository(IUnitOfWork uow, ISessionAccessor sessionAccessor) : EfRepository<SubscriptionUserEntity>(uow), ISubscriptionUserRepository
{
    public override IQueryable<SubscriptionUserEntity> GetAll()
    {
        return base.GetAll().Where(e => e.SubscriptionId == sessionAccessor.SubscriptionId);
    }

    public Task<SubscriptionUserEntity?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId)
        => GetAll()
            .Include(e => e.Subscription)
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.User.Email.Address == email && (subscriptionId == null || e.SubscriptionId == subscriptionId));
}