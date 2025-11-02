using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripEntity : TrotamundosBaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public string Subtitle { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public virtual List<AdvantageEntity> Advantages { get; private set; } = [];
    public virtual List<HighlightEntity> Highlights { get; private set; } = [];
    public virtual List<TripImageEntity> Images { get; private set; } = [];
    public virtual List<TripPaymentScheduleEntity> PaymentSchedules { get; private set; } = [];
    public virtual List<TripPriceIncludeEntity> PriceIncludes { get; private set; } = [];
    public virtual List<TripRequirementEntity> Requirements { get; private set; } = [];
    public virtual List<TripScheduleEntity> Schedules { get; private set; } = [];
    public virtual List<TripSuggestedFlightEntity> SuggestedFlights { get; private set; } = [];

    public static TripEntity Create(Guid tripId, TripDto model)
    {
        var trip = new TripEntity
        {
            Id = tripId
        };

        trip.Update(model);

        return trip;
    }

    public void Update(TripDto model)
    {
        Title = model.Title;
        Subtitle = model.Subtitle;
        Description = model.Description;

        Advantages.Clear();
        Advantages = model.Advantages
            .Select(advantageDto => AdvantageEntity.Create(Id, advantageDto))
            .ToList();

        Highlights.Clear();
        Highlights = model.Highlights
            .Select(highlightDto => HighlightEntity.Create(Id, highlightDto))
            .ToList();

        Images.Clear();
        Images = model.Images
            .Select(imageDto => TripImageEntity.Create(Id, imageDto))
            .ToList();

        PaymentSchedules.Clear();
        PaymentSchedules = model.PaymentSchedules
            .Select(paymentScheduleDto => TripPaymentScheduleEntity.Create(Id, paymentScheduleDto))
            .ToList();

        PriceIncludes.Clear();
        PriceIncludes = model.PriceIncludes
            .Select(priceIncludeDto => TripPriceIncludeEntity.Create(Id, priceIncludeDto))
            .ToList();

        Requirements.Clear();
        Requirements = model.Requirements
            .Select(requirementDto => TripRequirementEntity.Create(Id, requirementDto))
            .ToList();

        Schedules.Clear();
        Schedules = model.Schedules
            .Select(scheduleDto => TripScheduleEntity.Create(Id, scheduleDto))
            .ToList();

        SuggestedFlights.Clear();
        SuggestedFlights = model.SuggestedFlights
            .Select(suggestedFlightDto => TripSuggestedFlightEntity.Create(Id, suggestedFlightDto))
            .ToList();

    }
}
