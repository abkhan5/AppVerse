

using AppVerse.Conference.MsSql.Entity;
using AppVerse.Service.Authentication;

namespace AppVerse.Domain.Authentication.CommandHandler;

public class CreateUserCommandHandler : ICommandHandler<CreateProfile>
{
    private readonly IUserAuthenticationService authenticationService;
    private readonly IMessageSender messageSender;
    private readonly IUniqueCodeGeneratorService uniqueCodeGeneratorService;

    public CreateUserCommandHandler(IUniqueCodeGeneratorService uniqueCodeGeneratorService,
        IMessageSender messageSender, IUserAuthenticationService authenticationService)
    {
        this.uniqueCodeGeneratorService = uniqueCodeGeneratorService;
        this.messageSender = messageSender;
        this.authenticationService = authenticationService;
    }

    public async Task Handle(CreateProfile request, CancellationToken cancellationToken)
    {
        var user = new AppVerseUser
        {
            CreatedOn = DateTime.UtcNow,
            Email = request.EmailId,
            AccessFailedCount = 0,
            LockoutEnabled = false,
            TwoFactorEnabled = false,
            IsNdaSigned = false,
            ReferralCode = request.ReferalCode,
            UserName = request.EmailId,
            DisplayName = $"{request.FirstName} {request.LastName}",
            FirstName = request.FirstName,
            LastName = request.LastName,
            UpdatedOn = DateTime.UtcNow,
            EmailConfirmed = true
        };
        var password = request.Source == LoginSourceEnum.AppVerse ? request.Password : uniqueCodeGeneratorService.GetPassword();
        var result = await authenticationService.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await messageSender.SendMessage(
                new EveryEngUserCreatedDto(user.Email, user.Id, request.ReferalCode, request.Source),
                EveryEngUserCreatedDto.EveryEngUserCreated, cancellationToken);
        }
        else
        {
            List<ValidationFailure> messagesErrors = new();
            foreach (var error in result.Errors)
                messagesErrors.Add(new ValidationFailure(error.Code.Contains("Password") ? "Password" : error.Code, error.Description));
            throw new ValidationException(messagesErrors);
        }
    }
}