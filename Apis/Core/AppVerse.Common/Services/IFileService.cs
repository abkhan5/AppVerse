namespace AppVerse.Services;

public interface IFileService
{
    Task<byte[]> GetFile(BlobStorageItem storageItem, CancellationToken cancellationToken);
    Task<string> SaveAsync(string content, BlobStorageItem storageItem, CancellationToken cancellationToken);
    Task<string> SaveAsync(Stream content, BlobStorageItem storageItem, CancellationToken cancellationToken);
    Task DeleteAsync(BlobStorageItem storageItem, CancellationToken cancellationToken);
    Task DeleteFolderAsync(BlobStorageItem storageItem, CancellationToken cancellationToken);
}


public interface IUniqueCodeGeneratorService
{
    public string GetUniqueCode(string inputString = null);

    public string GetPassword();
}