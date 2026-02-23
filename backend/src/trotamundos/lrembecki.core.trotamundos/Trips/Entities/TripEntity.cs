using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.LoyalityPrograms;
using lrembecki.core.trotamundos.Trips.Dtos;
using lrembecki.core.ValueTypes;

namespace lrembecki.core.trotamundos.Trips.Entities;

internal class TripEntity : TrotamundosBaseEntity
{
    public string? Name { get; private set; }
    public bool IsActive { get; private set; } = true;
    public bool IsDisabled { get; private set; } = false;
    // New nullable date properties and calendar
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public string? Calendar { get; private set; } // max length50 to enforce in validation/persistence

    public string Title { get; private set; } = string.Empty;
    public string Subtitle { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string SuggestedFlightNotes { get; private set; } = string.Empty;
    public Price Price { get; private set; } = null!;

    public virtual List<TripAgendaEntity> Agenda { get; private set; } = [];
    public virtual List<AdvantageEntity> Advantages { get; private set; } = [];
    public virtual List<TripHighlightEntity> Highlights { get; private set; } = [];
    public virtual List<TripImageEntity> Images { get; private set; } = [];
    public virtual List<TripPaymentScheduleEntity> PaymentSchedules { get; private set; } = [];
    public virtual List<TripPriceIncludeEntity> PriceIncludes { get; private set; } = [];
    public virtual List<TripRequirementEntity> Requirements { get; private set; } = [];
    public virtual List<TripScheduleEntity> Schedules { get; private set; } = [];
    public virtual List<TripSuggestedFlightEntity> SuggestedFlights { get; private set; } = [];
    public virtual List<LoyalityProgramEntity> LoyalityPrograms { get; private set; } = [];

    public static TripEntity Create(Guid tripId, TripDto model)
    {
        var trip = new TripEntity
        {
            Id = tripId
        };

        trip.Update(model);

        trip.AddDomainEvent(TrotamundosDomainEvent.Create(trip));

        return trip;
    }

    public void UpdateAdvantages(List<AdvantageEntity> advantages)
    {
        Advantages.Clear();

        if (advantages.Count > 0)
        {
            Advantages.AddRange(advantages);
        }
    }

    public void Update(TripDto model)
    {
        Name = model.Name;
        IsActive = model.IsActive;
        IsDisabled = model.IsDisabled;
        StartDate = model.StartDate;
        EndDate = model.EndDate;
        Calendar = model.Calendar;
        Price = model.Price;

        Title = model.Title;
        Subtitle = model.Subtitle;
        Description = model.Description;
        SuggestedFlightNotes = model.SuggestedFlightNotes;

        AddDomainEvent(TrotamundosDomainEvent.Create(this));
    }
}
