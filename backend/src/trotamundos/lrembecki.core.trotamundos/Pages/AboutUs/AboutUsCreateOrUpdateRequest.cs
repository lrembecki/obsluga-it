using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsCreateOrUpdateRequest(AboutUsDto Model) : IRequest<AboutUsVM>
{
    public static Delegate Delegate => (AboutUsDto model, ISender sender, CancellationToken ct) => sender.Send(new AboutUsCreateOrUpdateRequest(model), ct).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<AboutUsCreateOrUpdateRequest, AboutUsVM>
    {
        public async Task<AboutUsVM> Handle(
            AboutUsCreateOrUpdateRequest request,
            CancellationToken ct
        )
        {
            var model = await request.Model.SyncStorageAsync(storage, ct)
                .With(e => e.SyncStorageCollectionAsync(storage, m => m.Persons, ct));

            var entities = await uow.GetRepository<AboutUsEntity>().GetAsync();
            var entity = entities.FirstOrDefault() ??
                await uow.GetRepository<AboutUsEntity>().AddAsync(
                    AboutUsEntity.Create(Guid.NewGuid(), model)
                );

            entity.Update(model);

            await uow.SaveChangesAsync(ct);

            return AboutUsVM.Map(entity);
        }
    }
}
