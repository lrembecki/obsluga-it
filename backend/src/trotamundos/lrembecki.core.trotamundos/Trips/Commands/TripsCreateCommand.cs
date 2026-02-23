using lrembecki.core.Services;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.LoyalityPrograms;
using lrembecki.core.trotamundos.Trips.Dtos;
using lrembecki.core.trotamundos.Trips.Entities;
using lrembecki.core.trotamundos.Trips.Helpers;
using lrembecki.core.trotamundos.Trips.ViewModels;

namespace lrembecki.core.trotamundos.Trips.Commands;

public record TripsCreateCommand(TripDto Model) : IRequest<TripVM>
{
    public static Delegate Delegate =>
        (TripDto model, ISender sender, CancellationToken ct) =>
            sender.Send(new TripsCreateCommand(model), ct).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<TripsCreateCommand, TripVM>
    {
        private readonly IRepository<TripEntity> _trips = uow.GetRepository<TripEntity>();
        private readonly IRepository<TripAgendaEntity> _agenda = uow.GetRepository<TripAgendaEntity>();
        private readonly IRepository<AdvantageEntity> _advantages = uow.GetRepository<AdvantageEntity>();
        private readonly IRepository<LoyalityProgramEntity> _loyalityPrograms = uow.GetRepository<LoyalityProgramEntity>(); 
        private readonly IRepository<TripImageEntity> _images = uow.GetRepository<TripImageEntity>();
        private readonly IRepository<TripHighlightEntity> _highlights = uow.GetRepository<TripHighlightEntity>();
        private readonly IRepository<TripPaymentScheduleEntity> _paymentSchedules = uow.GetRepository<TripPaymentScheduleEntity>();
        private readonly IRepository<TripPriceIncludeEntity> _priceIncludes = uow.GetRepository<TripPriceIncludeEntity>();
        private readonly IRepository<TripRequirementEntity> _requirements = uow.GetRepository<TripRequirementEntity>();
        private readonly IRepository<TripScheduleEntity> _schedules = uow.GetRepository<TripScheduleEntity>();
        private readonly IRepository<TripSuggestedFlightEntity> _suggestedFlights = uow.GetRepository<TripSuggestedFlightEntity>();

        public async Task<TripVM> Handle(TripsCreateCommand command, CancellationToken ct = default)
        {
            var model = command.Model;
            var entity = TripEntity.Create(Guid.NewGuid(), model);

            entity = await _trips.AddAsync(entity);

            await TripImageHelper.SyncImages(model, entity, storage, _suggestedFlights, _images, ct);

            var advantages = _advantages.GetAll(e => model.Advantages.Contains(e.Id)).ToList();
            entity.Advantages.AddRange(advantages);

            var loyalityPrograms = _loyalityPrograms.GetAll(e => model.LoyalityPrograms.Contains(e.Id)).ToList();
            entity.LoyalityPrograms.AddRange(loyalityPrograms);

            await _highlights.AddAsync(model.Highlights.Select(e => TripHighlightEntity.Create(entity.Id, e)).ToList());
            await _agenda.AddAsync(model.Agenda.Select(e => TripAgendaEntity.Create(entity.Id, e)).ToList());
            await _paymentSchedules.AddAsync(model.PaymentSchedules.Select(e => TripPaymentScheduleEntity.Create(entity.Id, e)).ToList());
            await _priceIncludes.AddAsync(model.PriceIncludes.Select(e => TripPriceIncludeEntity.Create(entity.Id, e)).ToList());
            await _requirements.AddAsync(model.Requirements.Select(e => TripRequirementEntity.Create(entity.Id, e)).ToList());
            await _suggestedFlights.AddAsync(model.SuggestedFlights.Select(e => TripSuggestedFlightEntity.Create(entity.Id, e)).ToList());
            await _images.AddAsync(model.Images.Select(e => TripImageEntity.Create(entity.Id, e)).ToList());
            await _schedules.AddAsync(model.Schedules.Select(e => TripScheduleEntity.Create(entity.Id, e)).ToList());

            await uow.SaveChangesAsync(ct);

            return TripVM.Map(entity);
        }
    }
}
