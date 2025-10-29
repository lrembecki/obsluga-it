namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record TripPaymentScheduleDto
{
    public int Order { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
