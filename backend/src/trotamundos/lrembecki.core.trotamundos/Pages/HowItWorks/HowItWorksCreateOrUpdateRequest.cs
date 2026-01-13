using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksCreateOrUpdateRequest(HowItWorksDto Model) : IRequest<HowItWorksVM>
{
    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<HowItWorksCreateOrUpdateRequest, HowItWorksVM>
    {
        public async Task<HowItWorksVM> Handle(
            HowItWorksCreateOrUpdateRequest request,
            CancellationToken cancellationToken
        )
        {
            var entities = await uow.GetRepository<HowItWorksEntity>().GetAsync();
            var entity = entities.FirstOrDefault();
            var model = request.Model;
            var items = model.Items ?? [];

            if (entity is null)
            {
                // Process images for items
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (item.Image is not null && item.ImageId == null)
                    {
                        var storedImage = await storage.CreateAsync(item.Image, cancellationToken);
                        items[i] = item with { ImageId = storedImage.Id };
                    }
                }

                entity = await uow.GetRepository<HowItWorksEntity>().AddAsync(
                    HowItWorksEntity.Create(Guid.NewGuid(), model)
                );
            }
            else
            {
                // Process images for items
                for (var i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    if (item.Image is not null)
                    {
                         if(item.ImageId == null || item.ImageId == Guid.Empty)
                         {
                             var storedImage = await storage.CreateAsync(item.Image, cancellationToken);
                             items[i] = item with { ImageId = storedImage.Id };
                         }
                         else
                         {
                             await storage.UpdateAsync(item.ImageId.Value, item.Image, cancellationToken);
                         }
                    }
                }

                entity.Update(model);
            }

            await uow.SaveChangesAsync(cancellationToken);

            return HowItWorksVM.Map(entity);
        }
    }
}
