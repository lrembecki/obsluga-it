using lrembecki.core.Services;
using lrembecki.core.storage.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace lrembecki.core.trotamundos.Services;

public interface ITripService : ICrudService<TripDto, TripVM>;
internal sealed class TripService(
    IUnitOfWork uow, 
    IStorageService storage) : BaseCrudService<TripEntity, TripVM, TripDto>(uow), ITripService
{
    private readonly IRepository<AdvantageEntity> _advantages = uow.GetRepository<AdvantageEntity>();

    protected override Task<TripDto> Validate(TripDto model)
        => base.Validate(CleanUpModel(model));

    protected override async Task<TripEntity> CreateEntity(Guid id, TripDto model, CancellationToken cancellationToken)
    {
        await SyncImagesAsync(model, null!, cancellationToken);
        var entity = await base.CreateEntity(id, model, cancellationToken);

        var advantages = _advantages.GetAll(e => model.Advantages.Contains(e.Id)).ToList();
        entity.UpdateAdvantages(advantages);

        return entity;
    }

    protected override async Task UpdateEntity(TripEntity entity, TripDto model)
    {
        await SyncImagesAsync(model, entity, default!);

        var advantages = _advantages.GetAll(e => model.Advantages.Contains(e.Id)).ToList();
        entity.UpdateAdvantages(advantages);

        await base.UpdateEntity(entity, model);
    }

    private TripDto CleanUpModel(TripDto model)
    {
        return model with
        {
            Images = model.Images.Where(e => e.Image != null).ToList(),
            Agenda = model.Agenda.Where(e => !string.IsNullOrEmpty(e.Content)).ToList()
        };
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
                await storage.UpdateAsync(image.ImageId.Value, image.Image, cancellationToken);
            }
        }
    }
}