namespace lrembecki.core.Services;

public interface ICrudService<Dto, VM>
{
    // queries
    Task<List<VM>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<VM>> GetAllAsync<TFilter>(TFilter filter, CancellationToken cancellationToken = default);

    // commands
    Task<VM> CreateAsync(Dto model, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<VM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VM> UpdateAsync(Guid id, Dto model, CancellationToken cancellationToken = default);
}
