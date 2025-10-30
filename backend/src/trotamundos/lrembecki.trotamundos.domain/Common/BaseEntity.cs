using lrembecki.shared.domain.Abstractions;

namespace lrembecki.trotamundos.domain.Common;

internal class BaseEntity : IHasSubscriptionId, IHasId<Guid>, IHasAuditFields
{
    public Guid Id { get; protected set; }
    public Guid SubscriptionId { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    public Guid? CreatedById { get; protected set; }

    public DateTime UpdatedAt { get; protected set; }

    public Guid? UpdatedById { get; protected set; }
}
