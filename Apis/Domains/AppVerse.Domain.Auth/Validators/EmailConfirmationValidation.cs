

namespace AppVerse.Domain.Authentication.Validation;

public class EmailConfirmationValidation : AbstractValidator<EmailConfirmation>
{
    private readonly IUserAuthenticationService authenticationService;
    private string _tokenErrorMessage;

    public EmailConfirmationValidation(IUserAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
        RuleFor(x => x.Token).NotEmpty().NotNull().WithMessage("{PropertyName} should be not empty!");
        RuleFor(x => x.UserEmail).NotEmpty().NotNull().WithMessage("{PropertyName} should be not empty!");
        RuleFor(x => x.UserEmail).EmailAddress().NotNull().WithMessage("{PropertyName} is not a valid email address!");
        RuleFor(x => x).MustAsync(IsEmailTokenValid).WithMessage(GetErrorMessage);
    }

    private string GetErrorMessage(EmailConfirmation model)
    {
        return _tokenErrorMessage;
    }

    public async Task<bool> IsEmailTokenValid(EmailConfirmation model, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByEmailAsync(model.UserEmail);
        var result = await authenticationService.ConfirmEmailAsync(user, model.Token);
        if (!result.Succeeded)
            foreach (var item in result.Errors)
                _tokenErrorMessage = item.Description;
        return result.Succeeded;
    }
}