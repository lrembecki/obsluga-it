using lrembecki.obsluga_it.application.Abstractions.Providers;

namespace lrembecki.obsluga_it.infrastructure.Providers;

internal class DateProvider : IDateProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Today => DateTime.Today.Date;
}
