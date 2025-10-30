using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.shared.application.Abstractions;

namespace lrembecki.obsluga_it.application.Contracts.Commands;

public record SubscriptionCreateCommand(string Name) : IRequest<SubscriptionVM>;
