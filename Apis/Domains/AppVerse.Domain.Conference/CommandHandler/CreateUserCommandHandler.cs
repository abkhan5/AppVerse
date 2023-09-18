
namespace AppVerse.Domain.Conference.CommandHandler;

public class CreateConferenceCommandHandler: ICommandHandler<CreateConference>
{
    private readonly IMessageSender messageSender;
    private readonly IUniqueCodeGeneratorService uniqueCodeGeneratorService;

    public CreateConferenceCommandHandler(IMessageSender messageSender, IUniqueCodeGeneratorService uniqueCodeGeneratorService)
    {
        this.messageSender = messageSender;
        this.uniqueCodeGeneratorService = uniqueCodeGeneratorService;
    }


    public async Task Handle(CreateConference request, CancellationToken cancellationToken)
    {

    }
}