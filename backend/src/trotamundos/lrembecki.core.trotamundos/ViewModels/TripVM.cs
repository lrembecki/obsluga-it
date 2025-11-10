using lrembecki.core.trotamundos.Entitites;

namespace lrembecki.core.trotamundos.ViewModels;

public record TripVM(
    Guid Id,
    string? Name,
    bool IsActive,
    bool IsDisabled,
    DateTime? StartDate,
    DateTime? EndDate,
    string? Calendar,
    string Title,
    string Subtitle,
    string Description,
    List<AdvantageVM> Advantages,
    List<TripHighlightVM> Highlights,
    List<TripImageVM> Images,
    List<TripPaymentScheduleVM> PaymentSchedules,
    List<TripPriceIncludeVM> PriceIncludes,
    List<TripRequirementVM> Requirements,
    List<TripScheduleVM> Schedules,
    List<TripSuggestedFlightVM> SuggestedFlights
)
{
    public static TripVM Map(TripEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripVM(
            entity.Id,
            entity.Name,
            entity.IsActive,
            entity.IsDisabled,
            entity.StartDate,
            entity.EndDate,
            entity.Calendar,
            entity.Title,
            entity.Subtitle,
            entity.Description,
            entity.Advantages.Select(AdvantageVM.Map).ToList(),
            entity.Highlights.Select(TripHighlightVM.Map).ToList(),
            entity.Images.Select(TripImageVM.Map).ToList(),
            entity.PaymentSchedules.Select(TripPaymentScheduleVM.Map).ToList(),
            entity.PriceIncludes.Select(TripPriceIncludeVM.Map).ToList(),
            entity.Requirements.Select(TripRequirementVM.Map).ToList(),
            entity.Schedules.Select(TripScheduleVM.Map).ToList(),
            entity.SuggestedFlights.Select(TripSuggestedFlightVM.Map).ToList()
        );
    }
}

