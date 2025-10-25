namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record FileVM (
	Guid FileId,
	Guid? FileGroupId,
	string Name,
	string? DisplayName,
	string? Description,
	string BlobPath,
	string Url,
	long Filesize,
	int Position
)
{
	internal static FileVM Map (domain.Entities.File file)
	{
		return new FileVM(
			file.FileId,
			file.GroupId,
			file.Name,
			file.DisplayName,
			file.Description,
			file.BlobPath,
			file.Url,
			file.Filesize,
			file.Position
		);
	}
}
