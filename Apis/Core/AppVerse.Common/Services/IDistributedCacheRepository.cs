namespace AppVerse.Services;

public interface IRealTimeService
{
    Task SendMessage(string hubMethod, object?[] payload, CancellationToken cancellationToken);
    Task StreamMessages<TEntity>(string hubMethod, Func<IAsyncEnumerable<TEntity>> stream, CancellationToken cancellationToken);

}
public static class HubEventNames
{
    public const string DomainRecordUpdated = "OnRecordUpdated";
    public const string RecordUpdated = "recordUpdated";
    public const string SearchResultReceiver = "ReceiveSearchResult";
    public const string SearchMetadata = "SearchMetadata";
    public const string SendNotification = "SendNotification";
    public const string CartUpdatedNotification = "CartUpdatedNotification";
}
