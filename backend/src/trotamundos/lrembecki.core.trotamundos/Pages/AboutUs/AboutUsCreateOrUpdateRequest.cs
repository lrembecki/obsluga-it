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
            var entities = await uow.GetRepository<AboutUsEntity>().GetAsync();
            var entity = entities.FirstOrDefault();
            var model = request.Model;

            if (entity is null)
            {
                model = model with
                {
                    ImageId = (await storage.CreateAsync(model.Image, cancellationToken)).Id
                };

                entity = await uow.GetRepository<AboutUsEntity>().AddAsync(
                    AboutUsEntity.Create(Guid.NewGuid(), model)
                );
            }
            else
            {
                await storage.UpdateAsync(entity.ImageId, model.Image, cancellationToken);
                entity.Update(model);
            }

            await uow.SaveChangesAsync(cancellationToken);

            return AboutUsVM.Map(entity);
        }
    }
}
