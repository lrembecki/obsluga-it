using lrembecki.trotamundos.domain.Common;

namespace lrembecki.trotamundos.domain.Entitites;

internal class SiteEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Title { get; private set; } = string.Empty;
}

internal class AboutUsSiteEntity
{
    public Guid SiteId { get; private set; }
    public SiteEntity Site { get; private set; } = default!;


}
