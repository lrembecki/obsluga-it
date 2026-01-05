using lrembecki.core.settings.Companies;
using lrembecki.core.storage;

namespace lrembecki.core.settings.Website;

internal class WebsiteMetaEntity
{
    public Guid WebsiteId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Keywords { get; private set; } = string.Empty;
    public Guid ImageId { get; private set; }
    public StorageEntity Image { get; private set; } = null!;
    
    public static WebsiteMetaEntity Create(Guid id, WebsiteMetaDto model)
    {
        var entity = new WebsiteMetaEntity
        {
            WebsiteId = id
        };
        entity.Update(model);
        return entity;
    }

    public void Update(WebsiteMetaDto model)
    {
        Title = model.Title;
        Description = model.Description;
        Keywords = model.Keywords;
        ImageId = model.ImageId!.Value;
    }
}