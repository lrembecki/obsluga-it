using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Extensions;

namespace lrembecki.obsluga_it.application.Services;

public interface ITagService : ICrudService<TagDto, TagVM>;
internal sealed class TagService(IUnitOfWork uow) : ITagService
{
    private readonly IRepository<TagEntity> _tags = uow.GetRepository<TagEntity>();

    public async Task<TagVM> CreateAsync(TagDto model, CancellationToken cancellationToken = default)
    {
        var tag = TagEntity.Create(Guid.NewGuid(), model.Name);
        await _tags.AddAsync(tag);
        return await GetByIdAsync(tag.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var tag = await _tags.RequireByIdAsync(id, cancellationToken: cancellationToken);
        await _tags.DeleteAsync(tag);
    }

    public Task<List<TagVM>> GetAllAsync(CancellationToken cancellationToken = default)
    => _tags.SelectAsync(e => TagVM.MapFromDomainEntity(e));

    public async Task<TagVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => TagVM.MapFromDomainEntity(await _tags.RequireByIdAsync(id, cancellationToken: cancellationToken));

    public async Task<TagVM> UpdateAsync(Guid id, TagDto model, CancellationToken cancellationToken = default)
    {
        var tag = await _tags.RequireByIdAsync(id, cancellationToken: cancellationToken);
        tag.Update(model.Name);
        await _tags.UpdateAsync(tag);
        return await GetByIdAsync(tag.Id, cancellationToken);
    }
}
