namespace AppVerse.Services;

public interface IDistributedCacheRepository
{
    Task<T> GetAsync<T>(string key, CancellationToken cancellationToken);
    Task SetEntityAsync<T>(string key, T entity, TimeSpan expiryTime, CancellationToken cancellationToken);
    Task SetLocalEntityAsync<T>(string key, T entity, TimeSpan expiryTime, CancellationToken cancellationToken);
    Task SetLocalEntitiesAsync<T>(string key, IEnumerable<T> entities, TimeSpan expiryTime, CancellationToken cancellationToken);

    Task SetAsync<T>(string key, IEnumerable<T> entities, CancellationToken cancellationToken);

    Task SetAsync<T>(string key, IEnumerable<T> entities, TimeSpan expiryTime, CancellationToken cancellationToken);

    Task RemoveAll(string key, CancellationToken cancellationToken);

    Task RemoveAll(List<string> key, CancellationToken cancellationToken);
    Task RefreshCache(string key, CancellationToken cancellationToken);
}