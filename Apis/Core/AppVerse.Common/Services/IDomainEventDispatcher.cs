﻿
namespace AppVerse.Services;

public interface IDomainEventDispatcher
{
    Task Dispatch(BaseDomainEvent domainEvent);
}
