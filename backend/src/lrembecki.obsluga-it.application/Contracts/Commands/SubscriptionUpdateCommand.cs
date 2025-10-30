using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.ViewModels;

namespace lrembecki.obsluga_it.application.Contracts.Commands;

public record SubscriptionUpdateCommand(Guid SubscriptionId, string Name) : IRequest<SubscriptionVM>;
