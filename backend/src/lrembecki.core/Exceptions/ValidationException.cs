namespace lrembecki.core.Exceptions;

public class ValidationException(Dictionary<string, string> model) : Exception()
{
    public Dictionary<string, string> Model { get; } = model;
}
