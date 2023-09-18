
namespace AppVerse.Domain.Authentication.CommandHandler;

public class UserCommandHandler :
    ICommandHandler<VerifyEmailCommand>
    , ICommandHandler<ForgotPasswordEmail>
    , ICommandHandler<ResetAndChangePassword>
    , ICommandHandler<ChangePassword>
    , ICommandHandler<EmailConfirmation>

{
    private readonly IUserAuthenticationService authenticationService;
    private readonly IIdentityService identityService;
    private readonly IMessageSender messageSender;

    public UserCommandHandler(
        IMessageSender messageSender,
        IUserAuthenticationService authenticationService,
        IIdentityService identityService)
    {
        this.messageSender = messageSender;
        this.authenticationService = authenticationService;
        this.identityService = identityService;
    }



    public async Task Handle(ChangePassword model, CancellationToken cancellationToken)
    {
        var userEmail = identityService.GetUserIdentity();
        var user = await authenticationService.FindByIdAsync(userEmail);

        var result =
            await authenticationService.ChangePasswordAsync(user, model.Password, model.UpdatedPassword);

        if (result.Succeeded)
            await SendEmailAsync(new CustomEmailDto
            {
                Link = new ShortEmailActionLinkDto(await authenticationService.GetLoginLinkAsync()),
                ProfileIds = new List<string> { user.Id }
            }, cancellationToken);
    }

    public async Task Handle(EmailConfirmation model, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(model.UserEmail);
        await SendEmailAsync(new CustomEmailDto
        {
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }

    public async Task Handle(ForgotPasswordEmail request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(request.EmailId);
        await SendEmailAsync(new CustomEmailDto
        {
            Link = new ShortEmailActionLinkDto(await authenticationService.GeneratePasswordResetTokenAsync(user)),
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }


    public async Task Handle(ResetAndChangePassword model, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(model.Email);
        var result = await authenticationService.ResetPasswordAsync(user, model.Token, model.Password);

        if (!result.Succeeded)
            return;

        await SendEmailAsync(new CustomEmailDto
        {
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }


    public async Task Handle(VerifyEmailCommand model, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(model.UserEmail);
        await SendEmailAsync(new CustomEmailDto
        {
            Link = new ShortEmailActionLinkDto(await authenticationService.GenerateSignupLink(user)),
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }

    private async Task SendEmailAsync(CustomEmailDto emailMetaDataDto, CancellationToken cancellationToken) =>
        await messageSender.SendMessage(emailMetaDataDto, cancellationToken);

}