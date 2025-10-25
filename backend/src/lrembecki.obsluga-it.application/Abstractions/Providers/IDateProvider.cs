namespace lrembecki.obsluga_it.application.Abstractions.Providers;

public interface IDateProvider
{
    DateTime UtcNow { get; }
    DateTime Today { get; }
}
