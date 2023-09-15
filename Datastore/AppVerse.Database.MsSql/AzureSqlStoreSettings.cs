namespace AppVerse.Database.MsSql;

internal sealed record AzureSqlStoreSettings
{
    public const string AzureSqlStoreOptions = "AzureSqlStoreSettings";

    public string ConnectionString { get; set; }

}
