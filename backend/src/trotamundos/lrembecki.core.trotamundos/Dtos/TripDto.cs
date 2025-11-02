namespace lrembecki.core.trotamundos.Dtos;

public record TripDto
{
    public string Title { get; init; } = string.Empty;
    public string Subtitle { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;

    public virtual List<AdvantageDto> Advantages { get; init; } = [];
    public virtual List<HighlightDto> Highlights { get; init; } = [];
    public virtual List<TripImageDto> Images { get; init; } = [];
    public virtual List<TripPaymentScheduleDto> PaymentSchedules { get; init; } = [];
    public virtual List<TripPriceIncludeDto> PriceIncludes { get; init; } = [];
    public virtual List<TripRequirementDto> Requirements { get; init; } = [];
    public virtual List<TripScheduleDto> Schedules { get; init; } = [];
    public virtual List<TripSuggestedFlightDto> SuggestedFlights { get; init; } = [];
}
