using MediatR;

namespace AppVerse.Infrastructure.Events;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
}

public interface IDomainEvent : INotification
{
}