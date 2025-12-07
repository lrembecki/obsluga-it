using lrembecki.core.Helpers;
using System.Text;
using System.Text.Json;

namespace lrembecki.functions.trotamundos.Helpers;

internal class UploadHelper(IBlobHelper blob)
{
    public async Task Upload<T>(T data, string blobPath, string fileName)
    {
        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        blob.UploadBlobAsync(
            "trotamundos",
            $"data/{blobPath}/{fileName}",
            $"{fileName}.json",
            ms,
            [],
            default
        ).Wait();
    }
}
