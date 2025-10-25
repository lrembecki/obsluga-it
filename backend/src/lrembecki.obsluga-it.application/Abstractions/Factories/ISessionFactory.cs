namespace lrembecki.obsluga_it.application.Abstractions.Factories;

public interface ISessionFactory
{
    bool IsAuthenticated { get; }
    string? Email { get; }
    Guid? SelectedSubscriptionId { get; }
}
