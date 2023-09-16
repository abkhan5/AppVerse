namespace AppVerse.Conference.MsSql;

internal sealed record AzureSqlStoreSettings
{
    public const string AzureSqlStoreOptions = "AzureSqlStoreSettings";


    public string AzureSqlConnectionString { get; set; }
}
