using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Specifications;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class SubscriptionUserRepository(IUnitOfWork uow, ISessionAccessor sessionAccessor) : EfRepository<SubscriptionUserEntity>(uow), ISubscriptionUserRepository
{
    public override IQueryable<SubscriptionUserEntity> GetAll(Specification<SubscriptionUserEntity> specification = null!)
    {
        return base.GetAll(specification.WithSubscriptionId(sessionAccessor.SubscriptionId));
    }

    public Task<SubscriptionUserEntity?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId)
    {
        IQueryable<SubscriptionUserEntity> query = GetAll()
            .Where(e => e.User.Email == email)
            .Include(e => e.Subscription)
            .Include(e => e.User);

        if (subscriptionId is not null)
        {
            query = query.Where(e => e.SubscriptionId == subscriptionId);
        }

        return query
            .FirstOrDefaultAsync();
    }
}