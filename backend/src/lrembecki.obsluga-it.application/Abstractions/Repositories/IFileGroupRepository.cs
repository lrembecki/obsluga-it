using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public interface IFileGroupRepository : IRepository
{
    Task<FileGroup?> GetByIdAsync(Guid id);
}