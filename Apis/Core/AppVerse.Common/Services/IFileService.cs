namespace AppVerse.Services;

public interface IFileService
{
    Task<byte[]> GetFile(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task<string> SaveAsync(string content, BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task<string> SaveAsync(Stream content, BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task DeleteAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task DeleteFolderAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
}
