using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.ViewModels;

namespace lrembecki.obsluga_it.application.Contracts.Commands;

public record SubscriptionCreateCommand(string Name) : IRequest<SubscriptionVM>;
