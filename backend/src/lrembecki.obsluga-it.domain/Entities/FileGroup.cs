using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.domain.Entities;

public class FileGroup : IHasSubscriptionId
{
    public Guid FileGroupId { get; set; }
    public Guid SubscriptionId { get; set; }
    public string Name { get; set; } = default!;

    public static FileGroup Create(Guid fileGroupId, string name)
    {
        return new FileGroup
        {
            FileGroupId = fileGroupId,
            Name = name
        };
    }
}
