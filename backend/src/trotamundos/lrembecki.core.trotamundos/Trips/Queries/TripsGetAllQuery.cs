using lrembecki.core.Services;
using lrembecki.core.trotamundos.Trips.Entities;
using lrembecki.core.trotamundos.Trips.ViewModels;

namespace lrembecki.core.trotamundos.Trips.Queries;

public record TripsGetAllQuery() : IRequest<List<TripListItemVM>>
{
    public static Delegate Delegate =>
        (ISender sender, CancellationToken ct) =>
            sender.Send(new TripsGetAllQuery(), ct).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow
    ) : IHandler<TripsGetAllQuery, List<TripListItemVM>>
    {
        private readonly IRepository<TripEntity> _trips = uow.GetRepository<TripEntity>();
        public Task<List<TripListItemVM>> Handle(TripsGetAllQuery query, CancellationToken ct = default)
        {
            var entities = _trips.GetAll().Select(e => TripListItemVM.Map(e)).ToList();
            return Task.FromResult(entities);
        }
    }
}
