using lrembecki.shared.application.Abstractions;

namespace lrembecki.shared.infrastructure.Providers;

internal class DateProvider : IDateProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Today => DateTime.Today.Date;
}
