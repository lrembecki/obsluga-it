using lrembecki.obsluga_it.domain.Abstractions;
using lrembecki.obsluga_it.domain.Common;

namespace lrembecki.obsluga_it.domain.Entities;

internal class LoyalityProgramEntity : SubscriptionBaseEntity, IHasId<Guid>
{
    public Guid Id { get; private set; }
    public ImageBlobEntity Image { get; set; } = default!;
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
