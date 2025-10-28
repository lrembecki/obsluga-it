using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Contracts.Queries;

public record LoyalityProgramsGetQuery() : IRequest<List<LoyalityProgramVM>>;

