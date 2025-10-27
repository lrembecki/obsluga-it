using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

internal interface ISubscriptionRepository : IRepository<SubscriptionEntity>
{
    Task<List<SubscriptionVM>> GetAllSubscriptionVMAsync(CancellationToken cancellationToken = default);
}
