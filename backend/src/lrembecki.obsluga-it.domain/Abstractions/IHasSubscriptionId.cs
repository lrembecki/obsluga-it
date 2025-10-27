using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.domain.Abstractions;

public interface IHasSubscriptionId
{
    Guid SubscriptionId { get; }
}
