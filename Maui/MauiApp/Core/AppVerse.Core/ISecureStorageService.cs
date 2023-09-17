
public interface ISecureStorageService
{
    Task SetAsync<T>(string key, T value) where T : class;


    Task<T> GetAsync<T>(string key) where T : class;

    Task<bool> RemoveValueAsync(string key);

    Task RemoveAllValueAsync();
}
