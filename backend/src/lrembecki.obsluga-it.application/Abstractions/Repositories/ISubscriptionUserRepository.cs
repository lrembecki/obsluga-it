using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

internal interface ISubscriptionUserRepository : IRepository<SubscriptionUserEntity>
{
    Task<SubscriptionUserEntity?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId);
}
