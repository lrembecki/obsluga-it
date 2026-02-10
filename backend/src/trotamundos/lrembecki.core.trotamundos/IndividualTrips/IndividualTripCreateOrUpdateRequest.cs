using lrembecki.core.Services;
using lrembecki.core.storage;

namespace lrembecki.core.trotamundos.IndividualTrips;

public sealed record IndividualTripCreateOrUpdateCommand(IndividualTripDto Model) : IRequest<IndividualTripVM>
{
    public static Delegate Delegate => (IndividualTripDto model, ISender sender, CancellationToken ct) => sender.Send(new IndividualTripCreateOrUpdateCommand(model), ct).ToServiceCallResultAsync();


    internal sealed class Handler(IUnitOfWork uow, IStorageService storage) : IHandler<IndividualTripCreateOrUpdateCommand, IndividualTripVM>
    {
        public async Task<IndividualTripVM> Handle(IndividualTripCreateOrUpdateCommand command, CancellationToken ct = default)
        {
            var repository = uow.GetRepository<IndividualTripEntity>();
            var entities = await repository.GetAsync();
            var entity = entities.SingleOrDefault();

            var model = await command.Model.SyncStorageCollectionAsync(storage, i => i.Items, ct);

            if (entity != null)
            {
                entity.Update(model);
                await repository.UpdateAsync(entity);
            }
            else
            {
                entity = IndividualTripEntity.Create(Guid.NewGuid(), model);
                await repository.AddAsync(entity);
            }

            await uow.SaveChangesAsync(ct);

            return IndividualTripVM.Map(entity);
        }
    }
}