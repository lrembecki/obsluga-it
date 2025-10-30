using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record TripVM(
    Guid Id,
    string Title,
    string Subtitle,
    string Description,
    List<AdvantageVM> Advantages,
    List<HighlightVM> Highlights,
    List<TripImageVM> Images,
    List<TripPaymentScheduleVM> PaymentSchedules,
    List<TripPriceIncludeVM> PriceIncludes,
    List<TripRequirementVM> Requirements,
    List<TripScheduleVM> Schedules,
    List<TripSuggestedFlightVM> SuggestedFlights
)
{
    public static TripVM MapFromDomainEntity(TripEntity entity)
    {
        if (entity == null)
        {
            return null!;
        }

        return new TripVM(
            entity.Id,
            entity.Title,
            entity.Subtitle,
            entity.Description,
            entity.Advantages.Select(AdvantageVM.MapFromDomainEntity).ToList(),
            entity.Highlights.Select(HighlightVM.MapFromDomainEntity).ToList(),
            entity.Images.Select(TripImageVM.MapFromDomainEntity).ToList(),
            entity.PaymentSchedules.Select(TripPaymentScheduleVM.MapFromDomainEntity).ToList(),
            entity.PriceIncludes.Select(TripPriceIncludeVM.MapFromDomainEntity).ToList(),
            entity.Requirements.Select(TripRequirementVM.MapFromDomainEntity).ToList(),
            entity.Schedules.Select(TripScheduleVM.MapFromDomainEntity).ToList(),
            entity.SuggestedFlights.Select(TripSuggestedFlightVM.MapFromDomainEntity).ToList()
        );
    }
}

