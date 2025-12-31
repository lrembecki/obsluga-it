using lrembecki.core.Helpers;

namespace lrembecki.core;

public static class BlobHelperExtensions
{
    public static async Task<string> ReadAllTextAsync(this IBlobHelper blob, string container, string blobPath, CancellationToken ct)
    {
        var stream = await blob.DownloadBlobAsync(container, blobPath, ct);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync(ct);
    }
}
