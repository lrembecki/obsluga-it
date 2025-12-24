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
    string SuggestedFlightNotes,
    List<TripAgendaVM> Agenda,
    List<Guid> Advantages,
    List<TripHighlightVM> Highlights,
    List<TripImageVM> Images,
    List<Guid> LoyalityPrograms,
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
            entity.SuggestedFlightNotes,
            [.. entity.Agenda.Select(TripAgendaVM.Map)],
            [.. entity.Advantages.Select(e => e.Id) ],
            [.. entity.Highlights.Select(TripHighlightVM.Map)],
            [.. entity.Images.Select(TripImageVM.Map)],
            [.. entity.LoyalityPrograms.Select(e => e.Id)],
            [.. entity.PaymentSchedules.Select(TripPaymentScheduleVM.Map)],
            [.. entity.PriceIncludes.Select(TripPriceIncludeVM.Map)],
            [.. entity.Requirements.Select(TripRequirementVM.Map)],
            [.. entity.Schedules.Select(TripScheduleVM.Map)],
            [.. entity.SuggestedFlights.Select(TripSuggestedFlightVM.Map)]
        );
    }
}

