using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Extensions;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Services;

public interface ITripService : ICrudService<TripDto, TripVM>;
internal sealed class TripService(IUnitOfWork uow) : ITripService
{
    private readonly IRepository<TripEntity> _trips = uow.GetRepository<TripEntity>();

    public async Task<TripVM> CreateAsync(TripDto model, CancellationToken cancellationToken = default)
    {
        var trip = TripEntity.Create(Guid.NewGuid(), model.Title, model.Subtitle, model.Description);
        await _trips.AddAsync(trip);
        return await GetByIdAsync(trip.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var trip = await _trips.RequireByIdAsync(id, cancellationToken: cancellationToken);
        await _trips.DeleteAsync(trip);
    }

    public Task<List<TripVM>> GetAllAsync(CancellationToken cancellationToken = default)
    => _trips.SelectAsync(e => TripVM.MapFromDomainEntity(e));

    public async Task<TripVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => TripVM.MapFromDomainEntity(await _trips.RequireByIdAsync(id, cancellationToken: cancellationToken));

    public async Task<TripVM> UpdateAsync(Guid id, TripDto model, CancellationToken cancellationToken = default)
    {
        var trip = await _trips.RequireByIdAsync(id, cancellationToken: cancellationToken);
        trip.Update(model.Title, model.Subtitle, model.Description);
        await _trips.UpdateAsync(trip);
        return await GetByIdAsync(trip.Id, cancellationToken);
    }
}
