using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class ImageBlobHandler(IUnitOfWork uow) : IRequestHandler<ImageBlobsGetQuery, List<ImageBlobVM>>
{
    public Task<List<ImageBlobVM>> HandleAsync(ImageBlobsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<IRepository<ImageBlobEntity>>()
    .GetAll()
    .Select(ImageBlobVM.MapFromDomainEntity)
    .ToList(), cancellationToken);
}
