using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public interface ISubscriptionUserRepository : IRepository
{
    Task<SubscriptionUser?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId);
}
