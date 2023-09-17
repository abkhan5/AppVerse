using AppVerse.Emails;

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
    private readonly IUserRepository userRepository;

    public UserCommandHandler(
        IMessageSender messageSender,
        IUserRepository userRepository,
        IUserAuthenticationService authenticationService,
        IIdentityService identityService)
    {
        this.messageSender = messageSender;
        this.userRepository = userRepository;
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
                EmailTemplateCode = EssentialEmailsEnum.ChangePassword.GetEnumDescription(),
                ProfileIds = new List<string> { user.Id }
            }, cancellationToken);
    }

    public async Task Handle(EmailConfirmation model, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(model.UserEmail);
        await userRepository.ConfirmEmail(user.Id, cancellationToken);
        await SendEmailAsync(new CustomEmailDto
        {
            EmailTemplateCode = EssentialEmailsEnum.ConfirmationLink.GetEnumDescription(),
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }

    public async Task Handle(ForgotPasswordEmail request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(request.EmailId);
        await SendEmailAsync(new CustomEmailDto
        {
            Link = new ShortEmailActionLinkDto(await authenticationService.GeneratePasswordResetTokenAsync(user)),
            EmailTemplateCode = EssentialEmailsEnum.ForgotPassword.GetEnumDescription(),
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
            EmailTemplateCode = EssentialEmailsEnum.ResetPassword.GetEnumDescription(),
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }


    public async Task Handle(VerifyEmailCommand model, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(model.UserEmail);
        await SendEmailAsync(new CustomEmailDto
        {
            Link = new ShortEmailActionLinkDto(await authenticationService.GenerateSignupLink(user)),
            EmailTemplateCode = EssentialEmailsEnum.ResendConfirmation.GetEnumDescription(),
            ProfileIds = new List<string> { user.Id }
        }, cancellationToken);
    }

    private async Task SendEmailAsync(CustomEmailDto emailMetaDataDto, CancellationToken cancellationToken) =>
        await messageSender.SendMessage(emailMetaDataDto, cancellationToken);

}