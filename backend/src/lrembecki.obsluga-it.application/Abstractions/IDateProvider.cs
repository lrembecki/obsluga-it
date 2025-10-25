namespace lrembecki.obsluga_it.application.Abstractions;

public interface IDateProvider
{
    DateTime UtcNow { get; }
    DateTime Today { get; }
}
