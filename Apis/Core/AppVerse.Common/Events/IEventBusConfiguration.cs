namespace AppVerse.Infrastructure.Events;

public interface IEventBusConfiguration
{
    string ServiceBusConfiguration { get; }
}