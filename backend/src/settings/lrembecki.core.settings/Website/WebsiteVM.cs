namespace lrembecki.core.settings.Website;

public record WebsiteVM(
    Guid Id,
    string Title,
    Guid CompanyId,
    WebsiteMetaVM Meta
)
{
    internal static WebsiteVM Map(WebsiteEntity entity)
    {
        if (entity is null) return null!;

        return new WebsiteVM(
            entity.Id,
            entity.Title,
            entity.CompanyId,
            WebsiteMetaVM.Map(entity.Meta)
        );
    }
}
