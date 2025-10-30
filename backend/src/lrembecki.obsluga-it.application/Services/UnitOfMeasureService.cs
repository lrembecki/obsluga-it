using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Extensions;

namespace lrembecki.obsluga_it.application.Services;

public interface IUnitOfMeasureService : ICrudService<UnitOfMeasureDto, UnitOfMeasureVM>;
internal sealed class UnitOfMeasureService(IUnitOfWork uow) : IUnitOfMeasureService
{
    private readonly IRepository<UnitOfMeasureEntity> _units = uow.GetRepository<UnitOfMeasureEntity>();

    public async Task<UnitOfMeasureVM> CreateAsync(UnitOfMeasureDto model, CancellationToken cancellationToken = default)
    {
        var unit = UnitOfMeasureEntity.Create(Guid.NewGuid(), model.Type, model.Name, model.ShortName, model.ShortCode);
        await _units.AddAsync(unit);
        return await GetByIdAsync(unit.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var unit = await _units.RequireByIdAsync(id, cancellationToken: cancellationToken);
        await _units.DeleteAsync(unit);
    }

    public Task<List<UnitOfMeasureVM>> GetAllAsync(CancellationToken cancellationToken = default)
    => _units.SelectAsync(e => UnitOfMeasureVM.MapFromDomainEntity(e));

    public async Task<UnitOfMeasureVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => UnitOfMeasureVM.MapFromDomainEntity(await _units.RequireByIdAsync(id, cancellationToken: cancellationToken));

    public async Task<UnitOfMeasureVM> UpdateAsync(Guid id, UnitOfMeasureDto model, CancellationToken cancellationToken = default)
    {
        var unit = await _units.RequireByIdAsync(id, cancellationToken: cancellationToken);
        unit.Update(model.Type, model.Name, model.ShortName, model.ShortCode);
        await _units.UpdateAsync(unit);
        return await GetByIdAsync(unit.Id, cancellationToken);
    }
}
