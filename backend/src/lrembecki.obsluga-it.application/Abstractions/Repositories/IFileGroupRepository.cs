using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public interface IFileGroupRepository : IRepository
{
    Task<List<FileGroupVM>> GetAllAsync(CancellationToken cancellationToken);
}