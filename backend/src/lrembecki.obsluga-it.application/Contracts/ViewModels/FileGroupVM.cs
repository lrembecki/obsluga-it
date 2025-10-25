using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record FileGroupVM(
	Guid GroupId,
	string Name)
{
	internal static FileGroupVM Map (FileGroup entity)
		=> new (
			entity.Id,
			entity.Name
		);
}
