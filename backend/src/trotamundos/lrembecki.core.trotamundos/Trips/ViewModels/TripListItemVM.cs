using lrembecki.core.trotamundos.Trips.Entities;

namespace lrembecki.core.trotamundos.Trips.ViewModels;

public record TripListItemVM(
    Guid Id,
    string? Name,
    string Title,
    string Subtitle,
    DateTime? StartDate,
    DateTime? EndDate,
    string? Calendar,
    bool IsActive,
    bool IsDisabled
)
{
    internal static TripListItemVM Map(TripEntity entity)
    {
        if (entity is null)
        {
            return null!;
        }

        return new TripListItemVM(
            Id: entity.Id,
            Name: entity.Name,
            Title: entity.Title,
            Subtitle: entity.Subtitle,
            StartDate: entity.StartDate,
            EndDate: entity.EndDate,
            Calendar: entity.Calendar,
            IsDisabled: entity.IsDisabled,
            IsActive: entity.IsActive
        );
    }
}
