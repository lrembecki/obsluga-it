using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Extensions;

namespace lrembecki.obsluga_it.application.Services;

public interface IHighlightService : ICrudService<HighlightDto, HighlightVM>;
internal sealed class HighlightService(IUnitOfWork uow) : IHighlightService
{
    private readonly IRepository<HighlightEntity> _highlights = uow.GetRepository<HighlightEntity>();

    public async Task<HighlightVM> CreateAsync(HighlightDto model, CancellationToken cancellationToken = default)
    {
        var highlight = HighlightEntity.Create(Guid.NewGuid(), model.Title, model.Icon);

        await _highlights.AddAsync(highlight);

        return await GetByIdAsync(highlight.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var highlight = await _highlights.RequireByIdAsync(id, cancellationToken: cancellationToken);
        await _highlights.DeleteAsync(highlight);
    }

    public Task<List<HighlightVM>> GetAllAsync(CancellationToken cancellationToken = default)
    => _highlights.SelectAsync(e => HighlightVM.MapFromDomainEntity(e));

    public async Task<HighlightVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => HighlightVM.MapFromDomainEntity(await _highlights.RequireByIdAsync(id, cancellationToken: cancellationToken));

    public async Task<HighlightVM> UpdateAsync(Guid id, HighlightDto model, CancellationToken cancellationToken = default)
    {
        var highlight = await _highlights.RequireByIdAsync(id, cancellationToken: cancellationToken);

        highlight.Update(model.Title, model.Icon);

        await _highlights.UpdateAsync(highlight);

        return await GetByIdAsync(highlight.Id, cancellationToken);
    }
}
