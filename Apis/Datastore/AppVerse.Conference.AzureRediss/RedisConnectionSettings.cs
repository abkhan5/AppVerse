namespace AppVerse.Storage.Redis;

public class RedisConnectionSettings
{
    public const string RedisConnectionOptionName = "RedisConnectionSettings";
    public string ConnectionString { get; set; }
}


public sealed record AzureRedisSettings
{
    public const string AzureRedisOptions = "AzureRedisSettings";
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}