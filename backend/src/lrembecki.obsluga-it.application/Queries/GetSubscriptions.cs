using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Queries;

public record GetSubscriptions() : IRequest<List<SubscriptionVM>>;
