using AppVerse;
using AppVerse.Storage.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class CacheExtension
{
    public static void AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDistributedCacheRepository, RedisCacheRepository>();
        services.Configure<RedisConnectionSettings>(configuration.GetSection(RedisConnectionSettings.RedisConnectionOptionName));
        services.Configure<AzureRedisSettings>(configuration.GetSection(AzureRedisSettings.AzureRedisOptions));
        var redisSettings = new AzureRedisSettings();
        configuration.GetSection(AzureRedisSettings.AzureRedisOptions).Bind(redisSettings);
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisSettings.ConnectionString;
            options.InstanceName = redisSettings.DatabaseName;
        });
    }

}
