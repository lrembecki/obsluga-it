using lrembecki.core.Services;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Trips.Dtos;
using lrembecki.core.trotamundos.Trips.Entities;

namespace lrembecki.core.trotamundos.Trips.Helpers;

internal static class TripImageHelper
{
    public static async Task SyncImages(
        TripDto model,
        TripEntity tripEntity,
        IStorageService storage,
        IRepository<TripSuggestedFlightEntity> suggestedFlights,
        IRepository<TripImageEntity> images,
        CancellationToken cancellationToken
    )
    {
        await SyncSuggestedFlightImages(model, tripEntity, storage, suggestedFlights, cancellationToken);
        await SyncImagesAsync(model, tripEntity, storage, images, cancellationToken);
    }

    private static async Task SyncSuggestedFlightImages(
        TripDto model, 
        TripEntity tripEntity, 
        IStorageService storage, 
        IRepository<TripSuggestedFlightEntity> suggestedFlights, 
        CancellationToken cancellationToken)
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

                await suggestedFlights.DeleteAsync(image);
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
    private static async Task SyncImagesAsync(
        TripDto model, 
        TripEntity tripEntity, 
        IStorageService storage, 
        IRepository<TripImageEntity> images, 
        CancellationToken cancellationToken)
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

                await images.DeleteAsync(image);
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
