namespace AppVerse.Domain.Authentication.CommandHandler;

public class SubscriberCommandHandler : ICommandHandler<SaveNewsLetterSubscription>
    , ICommandHandler<BookADemo>

{
    private readonly IAppRepository cmsRepository;

    public SubscriberCommandHandler(IAppRepository cmsRepository)
    {
        this.cmsRepository = cmsRepository;
    }

    public async Task Handle(SaveNewsLetterSubscription request, CancellationToken cancellationToken) =>
        await cmsRepository.SaveNewLetterSubscription(request.EmailId, cancellationToken);


    public async Task Handle(BookADemo request, CancellationToken cancellationToken)
    {
        await cmsRepository.BookADemo(request.EmailId, request.PhoneNumber, cancellationToken);
    }
}
