namespace AppVerse.Domain.Authentication.Validation;

public class IsEmailValidValidation : AbstractValidator<IsEmailValid>
{
    private readonly IUserAuthenticationService authenticationService;

    public IsEmailValidValidation(IUserAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
        RuleFor(x => x.UserEmail).NotEmpty().NotNull().WithMessage("Email should be not empty!");
        RuleFor(x => x.UserEmail).Must(IsValidEmail).WithMessage("Email is not a valid email address!");
        RuleFor(x => x.UserEmail).MustAsync(IsUnique).WithMessage("Email is taken !")
            .WithSeverity(Severity.Warning);
    }

    private bool IsValidEmail(string? emailString)
    {
        if (string.IsNullOrEmpty(emailString))
            return false;
        var isEmail = Regex.IsMatch(emailString,
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            RegexOptions.IgnoreCase);
        return isEmail;
    }

    private async Task<bool> IsUnique(string? emailId, CancellationToken cancellation)
    {
        var user = await authenticationService.FindByEmailAsync(emailId);
        return user == null;
    }
}