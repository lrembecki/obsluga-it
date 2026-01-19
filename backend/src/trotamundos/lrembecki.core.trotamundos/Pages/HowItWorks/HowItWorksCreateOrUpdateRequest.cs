using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.Pages.HowItWorks;

public record HowItWorksCreateOrUpdateRequest(HowItWorksDto Model) : IRequest<HowItWorksVM>
{
    public static Delegate Delegate => (HowItWorksDto model, ISender sender, CancellationToken ct) => sender.Send(new HowItWorksCreateOrUpdateRequest(model), ct).ToServiceCallResultAsync();

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
            var model = request.Model;
            
            var entity = entities.FirstOrDefault() ??
                await uow.GetRepository<HowItWorksEntity>().AddAsync(
                    HowItWorksEntity.Create(Guid.NewGuid(), model)
                );

            model = await model.SyncStorageCollectionAsync(storage, e => e.Items, cancellationToken);
            entity.Update(model);

            await uow.SaveChangesAsync(cancellationToken);

            return HowItWorksVM.Map(entity);
        }
    }
}
