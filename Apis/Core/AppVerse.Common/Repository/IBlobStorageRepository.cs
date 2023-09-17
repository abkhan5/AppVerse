using AppVerse;

namespace AppVerse;
public enum BlobStoreTypesEnums
{
    AppStore,
    VideoStore,
    AzureAssets
}

public interface IBlobStorageRepository
{
    Task SaveFile(BlobStorageItemDto storageItem, string url, CancellationToken cancellationToken);
    Task<string> GetEternalSharableLinkAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);

    Task<byte[]> GetFileAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task<byte[]> GetFileAsync(string containerName, string fileName, CancellationToken cancellationToken);
    Task<Stream> GetFileStreamAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task<Stream> GetFileStreamAsync(string containerName, string fileName, CancellationToken cancellationToken);
    Task<string> SaveAsync(Stream stream, BlobStorageItemDto storageItem, CancellationToken cancellationToken);

    Task SaveStreamAsync(Stream stream, BlobStorageItemDto storageItem, CancellationToken cancellationToken);

    Task CopyFiles(BlobStoreTypesEnums originSource, string originContainer, string destinationContainer, CancellationToken cancellationToken);
    IAsyncEnumerable<BlobStorageItemDto> GetAllFiles(CancellationToken cancellationToken);
    IAsyncEnumerable<BlobStorageItemDto> GetAllFiles(BlobStorageItemDto storageItem, CancellationToken cancellationToken);

    Task<IDictionary<string, string>> GetMetaData(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task<IDictionary<string, string>> GetMetaData(string containerName, string fileName, CancellationToken cancellationToken);
    Task SaveMetaData(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task SaveMetaData(string containerName, string fileName, IDictionary<string, string> metadata, CancellationToken cancellationToken);
    Task DeleteAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task DeleteFolderAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken);
    Task DeleteStoreAsync(string containerName, CancellationToken cancellationToken);

    IAsyncEnumerable<BlobStorageItemDto> GetFileListAsync(CancellationToken cancellationToken);
}