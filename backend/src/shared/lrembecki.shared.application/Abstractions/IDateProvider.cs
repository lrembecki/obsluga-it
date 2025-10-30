namespace lrembecki.shared.application.Abstractions;

public interface IDateProvider
{
    DateTime UtcNow { get; }
    DateTime Today { get; }
}
