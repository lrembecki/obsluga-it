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
    private readonly IRepository<TripImageEntity> _images = uow.GetRepository<TripImageEntity>();
    private readonly IRepository<TripHighlightEntity> _highlights = uow.GetRepository<TripHighlightEntity>();
    private readonly IRepository<TripPaymentScheduleEntity> _paymentSchedules = uow.GetRepository<TripPaymentScheduleEntity>();
    private readonly IRepository<TripPriceIncludeEntity> _priceIncludes = uow.GetRepository<TripPriceIncludeEntity>();
    private readonly IRepository<TripRequirementEntity> _requirements = uow.GetRepository<TripRequirementEntity>();
    private readonly IRepository<TripScheduleEntity> _schedules = uow.GetRepository<TripScheduleEntity>();
    private readonly IRepository<TripSuggestedFlightEntity> _suggestedFlights = uow.GetRepository<TripSuggestedFlightEntity>();


    protected override Task<TripDto> Validate(TripDto model)
        => base.Validate(CleanUpModel(model));

    protected override async Task<TripEntity> CreateEntity(Guid id, TripDto model, CancellationToken cancellationToken)
    {
        var entity = await base.CreateEntity(id, model, cancellationToken);

        await Task.WhenAll([
            SyncImagesAsync(model, entity, default!),
            SyncSuggestedFlightImages(model, entity, default!)
        ]);

        return entity;
    }

    protected override async Task UpdateEntity(TripEntity entity, TripDto model)
    {
        await Task.WhenAll([
            SyncImagesAsync(model, entity, default!),
            SyncSuggestedFlightImages(model, entity, default!)
        ]);

        await base.UpdateEntity(entity, model);

        var advantages = _advantages.GetAll(e => model.Advantages.Contains(e.Id)).ToList();
        entity.Advantages.Clear();
        await _advantages.DeleteAsync(entity.Advantages);
        entity.Advantages.AddRange(advantages);

        await _highlights.DeleteAsync(entity.Highlights);
        await _highlights.AddAsync(model.Highlights.Select(e => TripHighlightEntity.Create(entity.Id, e)).ToList());

        await _paymentSchedules.DeleteAsync(entity.PaymentSchedules);
        await _paymentSchedules.AddAsync(model.PaymentSchedules.Select(e => TripPaymentScheduleEntity.Create(entity.Id, e)).ToList());

        await _priceIncludes.DeleteAsync(entity.PriceIncludes);
        await _priceIncludes.AddAsync(model.PriceIncludes.Select(e => TripPriceIncludeEntity.Create(entity.Id, e)).ToList());

        await _requirements.DeleteAsync(entity.Requirements);
        await _requirements.AddAsync(model.Requirements.Select(e => TripRequirementEntity.Create(entity.Id, e)).ToList());

        await _suggestedFlights.DeleteAsync(entity.SuggestedFlights);
        await _suggestedFlights.AddAsync(model.SuggestedFlights.Select(e => TripSuggestedFlightEntity.Create(entity.Id, e)).ToList());

        await _images.DeleteAsync(entity.Images);
        await _images.AddAsync(model.Images.Select(e => TripImageEntity.Create(entity.Id, e)).ToList());

        await _schedules.DeleteAsync(entity.Schedules);
        await _schedules.AddAsync(model.Schedules.Select(e => TripScheduleEntity.Create(entity.Id, e)).ToList());
    }

    private TripDto CleanUpModel(TripDto model)
    {
        return model with
        {
            Images = model.Images.Where(e => e.Image != null).ToList(),
            Agenda = model.Agenda.Where(e => !string.IsNullOrEmpty(e.Content)).ToList(),
            SuggestedFlights = model.SuggestedFlights.Where(e => e.Image != null).ToList()
        };
    }

    private async Task SyncSuggestedFlightImages(TripDto model, TripEntity tripEntity, CancellationToken cancellationToken)
    {
        if (tripEntity != null)
        {
            var modelImageIds = model.SuggestedFlights
                .Where(m => m.ImageId != null!)
                .Select(m => m.ImageId!.Value)
                .ToList();

            var removeImages = tripEntity.SuggestedFlights
                .Where(e => !modelImageIds.Contains(e.ImageId))
                .ToList();

            foreach (var image in removeImages)
            {
                tripEntity.SuggestedFlights.Remove(image);

                await _suggestedFlights.DeleteAsync(image);
                await storage.DeleteAsync(image.ImageId, cancellationToken);
            }
        }

        for (int i = 0; i < model.SuggestedFlights.Count; i++)
        {
            var image = model.SuggestedFlights[i];
            if (image.ImageId is null)
            {
                var imageVM = await storage.CreateAsync(image.Image, cancellationToken);
                model.SuggestedFlights[i] = image with { ImageId = imageVM.Id };
            }
            else
            {
                await storage.UpdateAsync(image.ImageId.Value, image.Image, cancellationToken);
            }
        }
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

                await _images.DeleteAsync(image);
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