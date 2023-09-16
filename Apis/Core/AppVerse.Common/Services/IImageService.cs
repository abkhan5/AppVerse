namespace AppVerse.Services;

public interface IImageService
{
    Task<Stream> GenerateWebP(Stream inputStream, int width, int height, CancellationToken cancellationToken);
}
