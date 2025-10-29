using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Extensions;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Services;

public interface IAdvantageService : ICrudService<AdvantageDto, AdvantageVM>;
internal sealed class AdvantageService(IUnitOfWork uow) : IAdvantageService
{
    private readonly IRepository<AdvantageEntity> _advantages = uow.GetRepository<AdvantageEntity>();

    public async Task<AdvantageVM> CreateAsync(AdvantageDto model, CancellationToken cancellationToken = default)
    {
        var advantage = AdvantageEntity.Create(Guid.NewGuid(), model.Title, model.Content);

        await _advantages.AddAsync(advantage);

        return await GetByIdAsync(advantage.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var advantage = await _advantages.RequireByIdAsync(id, cancellationToken: cancellationToken);
        await _advantages.DeleteAsync(advantage);
    }

    public Task<List<AdvantageVM>> GetAllAsync(CancellationToken cancellationToken = default)
        => _advantages.SelectAsync(e => AdvantageVM.MapFromDomainEntity(e));

    public async Task<AdvantageVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => AdvantageVM.MapFromDomainEntity(await _advantages.RequireByIdAsync(id, cancellationToken: cancellationToken));

    public async Task<AdvantageVM> UpdateAsync(Guid id, AdvantageDto model, CancellationToken cancellationToken = default)
    {
        var advantage = await _advantages.RequireByIdAsync(id, cancellationToken: cancellationToken);

        advantage.Update(model.Title, model.Content);

        await _advantages.UpdateAsync(advantage);

        return await GetByIdAsync(advantage.Id, cancellationToken);
    }
}
