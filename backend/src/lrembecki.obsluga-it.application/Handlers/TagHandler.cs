using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class TagHandler(IUnitOfWork uow) : IRequestHandler<TagsGetQuery, List<TagVM>>
{
    public Task<List<TagVM>> HandleAsync(TagsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<TagEnity>()
        .GetAll()
        .Select(TagVM.MapFromDomainEntity)
        .ToList(), cancellationToken);
}
