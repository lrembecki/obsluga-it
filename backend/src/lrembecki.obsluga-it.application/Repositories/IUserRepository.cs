using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(Guid id);
    Task<List<SubscriptionVM>> GetAllAsync();
}

public interface ISubscriptionUserRepository
{
    Task<SubscriptionUser?> GetByEmailAndSubscriptionId(string email, Guid? subscriptionId);
}