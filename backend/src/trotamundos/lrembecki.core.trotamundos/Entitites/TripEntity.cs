using lrembecki.core.trotamundos.Common;
using lrembecki.core.trotamundos.DomainEvents;
using lrembecki.core.trotamundos.Dtos;

namespace lrembecki.core.trotamundos.Entitites;

public class TripEntity : TrotamundosBaseEntity
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

    public virtual List<TripAgendaEntity> Agenda { get; private set; } = [];
    public virtual List<AdvantageEntity> Advantages { get; private set; } = [];
    public virtual List<TripHighlightEntity> Highlights { get; private set; } = [];
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

        Title = model.Title;
        Subtitle = model.Subtitle;
        Description = model.Description;

        UpdateAgenda(model);
        UpdateImages(model);

        Highlights.Clear();
        Highlights = model.Highlights
            .Select(highlightDto => TripHighlightEntity.Create(Id, highlightDto))
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

        AddDomainEvent(TrotamundosDomainEvent.Create(this));
    }

    private void UpdateAgenda(TripDto model)
    {
        Agenda.Where(a => model.Agenda.All(dto => dto.Order != a.Order));

        model.Agenda.ForEach(agendaDto =>
        {
            var existing = Agenda.Find(e => e.Order == agendaDto.Order)
                ?? TripAgendaEntity.Create(Id, agendaDto);

            existing.Update(agendaDto);
        });
    }

    private void UpdateImages(TripDto model)
    {
        Images.Where(image => model.Images.All(dto => dto.ImageId != image.ImageId))
                    .ToList()
                    .ForEach(image => Images.Remove(image));

        model.Images.ForEach(imageDto =>
        {
            var existing = Images.Find(e => e.ImageId == imageDto.ImageId)
                ?? TripImageEntity.Create(Id, imageDto);

            existing.Update(imageDto);
        });
    }
}
