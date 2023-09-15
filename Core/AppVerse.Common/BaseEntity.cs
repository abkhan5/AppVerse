namespace AppVerse;

public interface IDomainEventDispatcher
{
    Task Dispatch(BaseDomainEvent domainEvent);
}
public abstract record BaseEntity<T> : BaseEntity
{
    [Key] public virtual T Id { get; set; }
}

public abstract record BaseEntity
{
    public List<BaseDomainEvent> Events = new();
}
public abstract record BaseDomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}