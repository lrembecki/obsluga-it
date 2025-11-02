using lrembecki.core.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface ITripService : ICrudService<TripDto, TripVM>;
internal sealed class TripService(IUnitOfWork uow) : BaseCrudService<TripEntity, TripVM, TripDto>(uow), ITripService
{
    protected override async Task UpdateEntity(TripEntity entity, TripDto model)
    {
        await base.UpdateEntity(entity, model);
    }
}