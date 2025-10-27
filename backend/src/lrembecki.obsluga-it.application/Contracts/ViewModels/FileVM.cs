using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record FileVM (
	Guid FileId,
	Guid? FileGroupId,
	string Filename,
	string? DisplayName,
	string? Description,
	string BlobPath,
	string BlobUrl,
	long? Size,
	int Position
)
{
	internal static FileVM Map (FileBlobEntity file)
	{
		if (file is null)
		{
			return null!;
		}

		return new FileVM(
			file.Id,
			file.GroupId,
			file.Filename,
			file.DisplayName,
			file.Description,
			file.BlobPath,
			file.BlobUrl,
			file.Size,
			file.Position
		);
	}
}
