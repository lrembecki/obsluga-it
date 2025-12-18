using lrembecki.core.Helpers;
using System.Text;
using System.Text.Json;

namespace lrembecki.function_app.Helpers;

internal class UploadHelper(IBlobHelper blob)
{
    public async Task Upload<T>(T data, string container, string blobPath, string fileName)
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        blob.UploadBlobAsync(
            container,
            $"published/{blobPath}/{fileName}",
            $"{fileName}.json",
            ms,
            [],
            default
        ).Wait();
    }

    public Task Remove(string container, string blobPath, string fileName)
        => blob.RemoveBlobAsync(
            container,
            $"published/{blobPath}/{fileName}.json",
            default
        );
}
