using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.AboutUs;

public record AboutUsCreateOrUpdateRequest(AboutUsDto Model) : IRequest<AboutUsVM>
{
    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<AboutUsCreateOrUpdateRequest, AboutUsVM>
    {
        public async Task<AboutUsVM> Handle(
            AboutUsCreateOrUpdateRequest request,
            CancellationToken cancellationToken
        )
        {
            var model = request.Model;
            var entities = await uow.GetRepository<AboutUsEntity>().GetAsync();
            var entity = entities.FirstOrDefault() ??
                await uow.GetRepository<AboutUsEntity>().AddAsync(
                    AboutUsEntity.Create(Guid.NewGuid(), model)
                );

            model = await model.SyncStorageAsync(storage, cancellationToken);
            model = await model.SyncStorageCollectionAsync(storage, m => m.Persons, cancellationToken);

            entity.Update(model);

            await uow.SaveChangesAsync(cancellationToken);

            return AboutUsVM.Map(entity);
        }
    }
}
