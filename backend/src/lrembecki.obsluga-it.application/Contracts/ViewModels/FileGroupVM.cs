using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record FileGroupVM(
	Guid GroupId,
	string Name)
{
	internal static FileGroupVM Map (FileGroupEntity entity)
		=> new (
			entity.Id,
			entity.Name
		);
}
