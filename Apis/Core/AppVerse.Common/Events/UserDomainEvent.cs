
namespace AppVerse.Infrastructure.Events;

public class UserDomainEvent : IDomainEvent
{
    public UserDomainEvent(UserEventDto @event)
    {
        UserEvent = @event;
    }

    public UserDomainEvent(string eventName, string message, string userId)
    {
        UserEvent = new UserEventDto
        {
            EventName = eventName,
            Message = message,
            UserId = userId
        };
    }

    public UserEventDto UserEvent { get; }
}