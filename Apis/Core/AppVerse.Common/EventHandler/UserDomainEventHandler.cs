
namespace AppVerse.Infrastructure.Events;

public class UserDomainEventHandler : IDomainEventHandler<UserDomainEvent>
{
    private readonly IIdentityService identityService;
    private readonly IUserEventStore userEventStore;

    public UserDomainEventHandler(IUserEventStore userEventStore, IIdentityService identityService)
    {
        this.userEventStore = userEventStore;
        this.identityService = identityService;
    }

    public async Task Handle(UserDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var userEvent = domainEvent.UserEvent;
        userEvent.Location = identityService.GetIpAddress().ToString();
        await userEventStore.Save(userEvent, cancellationToken);
    }
}