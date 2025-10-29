using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class HighlightHandler(IUnitOfWork uow) : IRequestHandler<HighlightsGetQuery, List<HighlightVM>>
{
    public Task<List<HighlightVM>> HandleAsync(HighlightsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<HighlightEntity>()
        .GetAll()
        .Select(HighlightVM.MapFromDomainEntity)
        .ToList(), cancellationToken);
}
