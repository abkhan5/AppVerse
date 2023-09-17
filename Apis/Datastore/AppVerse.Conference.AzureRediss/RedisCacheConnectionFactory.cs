using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace AppVerse.Storage.Redis;
public class RedisCacheRepository : IDistributedCacheRepository
{
    private readonly IDistributedCache cache;
    private readonly string redisOptions;
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<RedisCacheRepository> logger;
    public RedisCacheRepository(ILogger<RedisCacheRepository> logger, IDistributedCache cache, IOptions<AzureRedisSettings> redisOptions, IMemoryCache memoryCache)
    {
        this.cache = cache;
        this.redisOptions = redisOptions.Value.DatabaseName + "~";
        this.memoryCache = memoryCache;
        this.logger = logger;
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken)
    {
        try
        {
            var cacheData = memoryCache.Get<T>(key);
            if (cacheData != null)
                return cacheData;

            var response = await cache.GetAsync(redisOptions + key, cancellationToken);
            if (response == null)
                return default;
            var decompressed = (await Encoding.UTF8.GetString(response).Decompress(cancellationToken));
            return decompressed.FromJson<T>();
        }
        catch (Exception e)
        {
            memoryCache.Remove(key);
            await cache.RemoveAsync(redisOptions + key, cancellationToken);
            logger.LogError(e, "Error for key {error}", key);
            return default;
        }
    }

    public async Task RemoveAll(string key, CancellationToken cancellationToken)
    {
        key = redisOptions + key;
        await cache.RemoveAsync(key, cancellationToken);
    }

    public async Task RemoveAll(List<string> keys, CancellationToken cancellationToken)
    {
        foreach (var key in keys)
            await cache.RemoveAsync($"{redisOptions}{key}", cancellationToken);
    }


    public async Task RefreshCache(string key, CancellationToken cancellationToken)
    {
        await cache.RefreshAsync(redisOptions + key, cancellationToken);
    }

    #region Set to Cache

    public async Task SetEntityAsync<T>(string key, T entity, TimeSpan expiryTime,
        CancellationToken cancellationToken)
    {
        key = redisOptions + key;
        var serializedData = entity.ToJson();
        var compressedData = await serializedData.Compress(cancellationToken);
        var dataStream = Encoding.UTF8.GetBytes(compressedData);
        await cache.SetAsync(key, dataStream, new DistributedCacheEntryOptions { SlidingExpiration = expiryTime },
            cancellationToken);
    }
    public async Task SetLocalEntityAsync<T>(string key, T entity, TimeSpan expiryTime, CancellationToken cancellationToken)
    {
        memoryCache.Set(key, entity, new TimeSpan(0, 15, 0));
        await SetEntityAsync(key, entity, expiryTime, cancellationToken);
    }

    public async Task SetLocalEntitiesAsync<T>(string key, IEnumerable<T> entities, TimeSpan expiryTime, CancellationToken cancellationToken)
    {
        memoryCache.Set(key, entities, new TimeSpan(0, 15, 0));
        await SetEntityAsync(key, entities, expiryTime, cancellationToken);
    }


    public async Task SetAsync<T>(string key, IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        if (entities == null)
            return;
        key = redisOptions + key;
        var serializedData = JsonSerializer.Serialize(entities);
        var compressedData = await serializedData.Compress(cancellationToken);
        var dataStream = Encoding.UTF8.GetBytes(compressedData);
        await cache.SetAsync(key, dataStream, cancellationToken);
    }

    public async Task SetAsync<T>(string key, IEnumerable<T> entities, TimeSpan expiryTime,
        CancellationToken cancellationToken)
    {
        if (entities == null)
            return;
        key = redisOptions + key;
        var serializedData = JsonSerializer.Serialize(entities);
        var compressedData = await serializedData.Compress(cancellationToken);
        var dataStream = Encoding.UTF8.GetBytes(compressedData);
        await cache.SetAsync(key, dataStream, new DistributedCacheEntryOptions { SlidingExpiration = expiryTime },
            cancellationToken);
    }


    public async Task SetEntityAsync<T>(string key, T entity, CancellationToken cancellationToken)
    {
        key = redisOptions + key;
        var serializedData = JsonSerializer.Serialize(entity);
        var compressedData = await serializedData.Compress(cancellationToken);
        var dataStream = Encoding.UTF8.GetBytes(compressedData);
        await cache.SetAsync(key, dataStream, cancellationToken);
    }

    #endregion
}