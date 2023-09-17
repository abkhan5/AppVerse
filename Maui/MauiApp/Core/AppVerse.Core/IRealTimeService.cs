public interface IRealTimeService
{
    Task Start(CancellationToken cancellationToken);
    Task Sender(string hubMethod, object?[] payload, CancellationToken cancellationToken);
    void Receiver<T>(string clientReceiver, Func<T, Task> receiver);
}

public interface IRestService
{
    void Initialize(string serviceName);
    Task<T> Get<T>(string resource, int cacheDuration, CancellationToken cancellationToken);
    Task Post<T>(string uri, T payload);
    Task Put<T>(string uri, T payload);
    Task Delete(string uri);
}
