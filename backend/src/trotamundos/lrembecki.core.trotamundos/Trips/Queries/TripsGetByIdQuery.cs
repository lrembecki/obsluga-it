using lrembecki.core.Services;
using lrembecki.core.trotamundos.Trips.Entities;
using lrembecki.core.trotamundos.Trips.ViewModels;

namespace lrembecki.core.trotamundos.Trips.Queries;

public record TripsGetByIdQuery(Guid Id) : IRequest<TripVM>
{
    public static Delegate Delegate =>
        (Guid id, ISender sender, CancellationToken ct) =>
            sender.Send(new TripsGetByIdQuery(id), ct).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow
    ) : IHandler<TripsGetByIdQuery, TripVM>
    {
        private readonly IRepository<TripEntity> _trips = uow.GetRepository<TripEntity>();
        public async Task<TripVM> Handle(TripsGetByIdQuery query, CancellationToken ct = default)
        {
            var entity = await _trips.RequireByIdAsync(query.Id, ct);
            return TripVM.Map(entity);
        }
    }
}
