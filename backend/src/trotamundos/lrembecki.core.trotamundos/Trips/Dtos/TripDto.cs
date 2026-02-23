using lrembecki.core.ValueTypes;

namespace lrembecki.core.trotamundos.Trips.Dtos;

public record TripDto
{
    public string? Name { get; init; }
    public bool IsActive { get; init; } = true;
    public bool IsDisabled { get; init; } = false;
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Calendar { get; init; } // max length50
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string SuggestedFlightNotes { get; init; } = string.Empty;
    public Price Price { get; init; } = null!;

    public virtual List<TripAgendaDto> Agenda { get; init; } = [];
    public virtual List<Guid> Advantages { get; init; } = [];
    public virtual List<TripHighlightDto> Highlights { get; init; } = [];
    public virtual List<TripImageDto> Images { get; init; } = [];
    public virtual List<Guid> LoyalityPrograms { get; init; } = [];
    public virtual List<TripPaymentScheduleDto> PaymentSchedules { get; init; } = [];
    public virtual List<TripPriceIncludeDto> PriceIncludes { get; init; } = [];
    public virtual List<TripRequirementDto> Requirements { get; init; } = [];
    public virtual List<TripScheduleDto> Schedules { get; init; } = [];
    public virtual List<TripSuggestedFlightDto> SuggestedFlights { get; init; } = [];
}
