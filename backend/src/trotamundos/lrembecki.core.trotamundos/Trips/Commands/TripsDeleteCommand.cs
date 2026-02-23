using lrembecki.core.Services;
using lrembecki.core.storage;
using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.LoyalityPrograms;
using lrembecki.core.trotamundos.Trips.Entities;

namespace lrembecki.core.trotamundos.Trips.Commands;

internal record TripsDeleteCommand(Guid Id) : IRequest<bool>
{
    public static Delegate Delegate =>
        (Guid id, ISender sender, CancellationToken ct) =>
            sender.Send(new TripsDeleteCommand(id), ct).ToServiceCallResultAsync();

    internal sealed class Handler(
        IUnitOfWork uow,
        IStorageService storage
    ) : IHandler<TripsDeleteCommand, bool>
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
        public async Task<bool> Handle(TripsDeleteCommand command, CancellationToken ct = default)
        {
            var tripEntity = await _trips.RequireByIdAsync(command.Id, ct);

            var images = tripEntity.Images.Select(e => e.Image).ToList()
                .Concat(tripEntity.SuggestedFlights.Select(e => e.Image))
                .ToList();


            //tripEntity.Images.Clear();
            //tripEntity.SuggestedFlights.Clear();
            tripEntity.Advantages.Clear();
            tripEntity.LoyalityPrograms.Clear();

            await _agenda.DeleteAsync(tripEntity.Agenda);
            await _highlights.DeleteAsync(tripEntity.Highlights);
            await _paymentSchedules.DeleteAsync(tripEntity.PaymentSchedules);
            await _priceIncludes.DeleteAsync(tripEntity.PriceIncludes);
            await _requirements.DeleteAsync(tripEntity.Requirements);
            await _suggestedFlights.DeleteAsync(tripEntity.SuggestedFlights);
            await _images.DeleteAsync(tripEntity.Images);
            await _schedules.DeleteAsync(tripEntity.Schedules);

            await _trips.DeleteAsync(tripEntity);
            await uow.SaveChangesAsync(ct);


            foreach (var image in images)
            {
                await storage.DeleteAsync(image.Id, ct);
            }

            return true;
        }
    }
}
