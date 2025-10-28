using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Contracts.Commands;

public record SubscriptionCreateCommand(string Name) : IRequest<SubscriptionVM>;
