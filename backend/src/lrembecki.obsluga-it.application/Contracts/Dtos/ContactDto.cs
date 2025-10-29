namespace lrembecki.obsluga_it.application.Contracts.Dtos;

public record ContactDto
{
    public Guid? Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
