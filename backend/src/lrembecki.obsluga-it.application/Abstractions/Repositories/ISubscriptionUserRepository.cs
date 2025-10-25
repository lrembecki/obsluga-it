using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public interface ISubscriptionUserRepository : IRepository<SubscriptionUser>
{
    Task<SubscriptionUser?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId);
}
