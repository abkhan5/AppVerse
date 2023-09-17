namespace AppVerse.Storage.AzureCosmosDbSql;

internal record QueryHistoryRecords(string query, string continuationToken, int maxRecords, int pageNumber, int totalRecords)
{
    public string nextToken { get; set; }
};