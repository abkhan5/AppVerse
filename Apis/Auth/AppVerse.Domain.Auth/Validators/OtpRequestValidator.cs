

namespace AppVerse.Domain.Authentication.Validation;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private readonly DbContext context;
    public LoginRequestValidator(DbContext context)
    {
        this.context = context;
        RuleFor(x => x.UserLoginInput).MustAsync(UserExists).WithErrorCode("ErrorAuth102");
    }

    private async Task<bool> UserExists(string userLoginInput, CancellationToken cancellationToken)
    => await context.Set<AppVerseUser>().AnyAsync(item => item.PhoneNumber == userLoginInput || item.Email == userLoginInput, cancellationToken);
}
