namespace AppVerse.Services;

public interface IMessageReceiver
{
    public string QueueName { get; }
    Task OnMessageAsync(string queueName, Func<CancellationToken, Task> callBack, CancellationToken token);
    //   Task OnMessageAsync<T>(string queueName, Func<T, Task> callBack, CancellationToken token);
    Task OnMessageAsync<T>(string queueName, Func<T, CancellationToken, Task> callBack, CancellationToken token);

    Task Stop(CancellationToken token);
}
public interface IEventBusService
{
    //Task SetupSubscription<T>(string subscriptionName, string topicName, Func<T, CancellationToken, Task<bool>> callBack, CancellationToken cancellationToken);

    Task Subscribe<T>(string topicName, string subscriptionName, bool toForward, Func<T, CancellationToken, Task<bool>> callBack, CancellationToken cancellationToken);
    Task Subscribe<T>(string topicName, string subscriptionName, bool toForward, Func<T, CancellationToken, Task<bool>> callBack, IDictionary<string, string> traits, CancellationToken cancellationToken);

    Task Publish<T>(T domainEvent, string topicName, CancellationToken token);
    Task Publish<T>(T domainEvent, string topicName, IDictionary<string, string> traits, CancellationToken token);
}