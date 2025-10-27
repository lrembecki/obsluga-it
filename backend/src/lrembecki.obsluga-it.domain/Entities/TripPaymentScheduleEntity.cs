namespace lrembecki.obsluga_it.domain.Entities;

internal class TripPaymentScheduleEntity
{
    public Guid TripId { get; private set; }
    public int Order { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Price { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
}
