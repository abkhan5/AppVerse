using AppVerse.Services;

namespace AppVerse.Conference.AzureBlobStore;

public class AppRepository : IAppRepository
{
    public Task BookADemo(string emailId, int? phoneNumber, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveNewLetterSubscription(string emailId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class FileService : IFileService
{
    public Task DeleteAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFolderAsync(BlobStorageItemDto storageItem, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> GetFile(BlobStorageItemDto storageItem, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> SaveAsync(string content, BlobStorageItemDto storageItem, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> SaveAsync(Stream content, BlobStorageItemDto storageItem, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public class UserEventStore : IUserEventStore
{
    public async Task Save(UserEventDto userEvent, CancellationToken cancellationToken)
    {
    }
}