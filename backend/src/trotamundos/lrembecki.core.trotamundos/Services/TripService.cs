using lrembecki.core.Services;
using lrembecki.core.storage.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface ITripService : ICrudService<TripDto, TripVM>;
internal sealed class TripService(
    IUnitOfWork uow, 
    IStorageService storage) : BaseCrudService<TripEntity, TripVM, TripDto>(uow), ITripService
{
    protected override async Task<TripEntity> CreateEntity(Guid id, TripDto model, CancellationToken cancellationToken)
    {
        await SyncImagesAsync(model, null!, cancellationToken);
        return await base.CreateEntity(id, model, cancellationToken);
    }

    protected override async Task UpdateEntity(TripEntity entity, TripDto model)
    {
        await SyncImagesAsync(model, entity, default!);
        await base.UpdateEntity(entity, model);
    }

    private async Task SyncImagesAsync(TripDto model, TripEntity tripEntity, CancellationToken cancellationToken)
    {
        if (tripEntity != null)
        {
            var modelImageIds = model.Images
                .Where(m => m.ImageId != null!)
                .Select(m => m.ImageId!.Value)
                .ToList();

            var removeImages = tripEntity.Images
                .Where(e => !modelImageIds.Contains(e.ImageId))
                .ToList();

            foreach (var image in removeImages)
            {
                tripEntity.Images.Remove(image);
                await storage.DeleteAsync(image.ImageId, cancellationToken);
            }
        }

        for (int i = 0; i < model.Images.Count; i++)
        {
            var image = model.Images[i];
            if (image.ImageId is null)
            {
                var imageVM = await storage.CreateAsync(image.Image, cancellationToken);
                model.Images[i] = image with { ImageId = imageVM.Id };
            }
            else
            {
                var imageVM = await storage.UpdateAsync(image.ImageId.Value, image.Image, cancellationToken);
            }
        }
    }
}