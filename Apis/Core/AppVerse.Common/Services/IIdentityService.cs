using AppVerse.Base;
using System.Net;

namespace AppVerse.Services;

public interface IIdentityService
{
    public string GetUserIdentity();
    public string GetUserEmail();
    IPAddress GetIpAddress();
    public IPAddress GetLocalAddress();
}
public interface IUserEventStore
{
    Task Save(UserEventDto userEvent, CancellationToken cancellationToken);

    // IAsyncEnumerable<UserEventDto> Get(string userId, int? numberOfItems, CancellationToken cancellationToken);
}

public interface IQueueOperations
{
    Task DeleteAll(CancellationToken cancellation);
}