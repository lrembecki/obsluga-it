using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Queries;

public record FileGroupsGet(): IRequest<List<FileGroupVM>>;
internal sealed class FileGroupsGetHandler(IUnitOfWork uow) : IRequestHandler<FileGroupsGet, List<FileGroupVM>>
{
    public Task<List<FileGroupVM>> HandleAsync(FileGroupsGet request, CancellationToken cancellationToken = default)
        => uow.GetRepository<IFileGroupRepository>().GetAllAsync(cancellationToken);
}
