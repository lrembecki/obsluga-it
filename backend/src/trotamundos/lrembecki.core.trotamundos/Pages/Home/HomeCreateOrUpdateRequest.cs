using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.Home;

public record HomeCreateOrUpdateRequest(HomeDto Model) : IRequest<HomeVM>
{
    public static Delegate Delegate =>
        (HomeCreateOrUpdateRequest request, ISender sender, CancellationToken ct) =>
            sender.Send(request, ct).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<HomeCreateOrUpdateRequest, HomeVM>
    {
        public async Task<HomeVM> Handle(
            HomeCreateOrUpdateRequest request,
            CancellationToken ct
        )
        {
            var model = request.Model;

            var background = await storage.SyncAsync(model.BackgroundVideoId, model.BackgroundVideo, ct);
            var trailer = await storage.SyncAsync(model.TrailerVideoId, model.TrailerVideo, ct);

            model = model with
            {
                BackgroundVideoId = background?.Id ?? Guid.Empty,
                TrailerVideoId = trailer?.Id ?? Guid.Empty
            };

            model = await model.SyncStorageCollectionAsync(storage, m => m.Opinions, ct);

            var entities = await uow.GetRepository<HomeEntity>().GetAsync();
            var entity = entities.FirstOrDefault() ??
                await uow.GetRepository<HomeEntity>().AddAsync(
                    HomeEntity.Create(Guid.NewGuid(), model)
                );

            entity.Update(model);

            await uow.SaveChangesAsync(ct);

            return HomeVM.Map(entity);
        }
    }
}
