using AppVerse.Base;

namespace AppVerse.Services;

public interface IDomainEventDispatcher
{
    Task Dispatch(BaseDomainEvent domainEvent);
}

public interface IMessageSender
{
    Task SendMessage<T>(T message, CancellationToken cancellationToken);

    Task SendMessage<T>(T message, string queueName, CancellationToken cancellationToken);
    Task SendMessage<T>(MessagePayload<T> message, string queueName, CancellationToken cancellationToken);

    Task SendMessages<T>(IEnumerable<T> messages, string queueName, CancellationToken cancellationToken);

    Task CancelMessage(string messageId, CancellationToken cancellationToken);
    Task Close(CancellationToken cancellationToken);
}

public record MessagePayload<T>
{
    public T Payload { get; set; }

    public string Id { get; set; }
    public DateTimeOffset? ScheduledEnqueueTime { get; set; }
}