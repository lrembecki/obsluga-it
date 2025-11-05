namespace lrembecki.core.Services
{
    public interface ICrudService<Dto, VM>
    {
        Task<VM> CreateAsync(Dto model, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<List<VM>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<VM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<VM> UpdateAsync(Guid id, Dto model, CancellationToken cancellationToken = default);
    }
}
