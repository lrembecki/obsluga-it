using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.settings.Website;


public interface IWebsiteService : ICrudService<WebsiteDto, WebsiteVM>;
internal sealed class WebsiteService(IUnitOfWork uow, IStorageService storage) : BaseCrudService<WebsiteEntity, WebsiteVM, WebsiteDto>(uow), IWebsiteService
{
    protected override async Task<WebsiteEntity> CreateEntity(Guid id, WebsiteDto model, CancellationToken cancellationToken)
    {
        model = model with
        {
            Meta = model.Meta with
            {
                ImageId = (await storage.CreateAsync(model.Meta.Image, cancellationToken)).Id
            }
        };

        var entity = await base.CreateEntity(id, model, cancellationToken);
        return entity;
    }

    protected override async Task UpdateEntity(WebsiteEntity entity, WebsiteDto model)
    {
        await storage.UpdateAsync(entity.Meta.ImageId, model.Meta.Image);
        await base.UpdateEntity(entity, model);
    }
}
