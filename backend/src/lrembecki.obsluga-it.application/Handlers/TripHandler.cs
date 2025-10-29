using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class TripHandler(IUnitOfWork uow) : IRequestHandler<TripsGetQuery, List<TripVM>>
{
    public Task<List<TripVM>> HandleAsync(TripsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<TripEntity>()
        .GetAll()
        .Select(TripVM.MapFromDomainEntity)
        .ToList(), cancellationToken);
}
