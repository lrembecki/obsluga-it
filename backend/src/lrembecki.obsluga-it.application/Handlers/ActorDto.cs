namespace lrembecki.obsluga_it.application.Handlers;

public record ActorDto
{
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public List<ContactDto> Contacts { get; set; } = [];
}
