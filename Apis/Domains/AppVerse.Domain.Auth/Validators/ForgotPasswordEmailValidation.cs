

namespace AppVerse.Domain.Authentication.Validation;

public class ForgotPasswordEmailValidation : AbstractValidator<ForgotPasswordEmail>
{
    private readonly DbContext context;

    public ForgotPasswordEmailValidation(DbContext context)
    {
        this.context = context;
        RuleFor(x => x.EmailId).MustAsync(ValidateUser).WithErrorCode(EveryEngErrorRegistry.ErrorAuth101);
        RuleFor(x => x.EmailId).NotEmptyOrNullRule();
        RuleFor(x => x.EmailId).EmailAddress().WithMessage("{PropertyName} is not a valid email address!");
    }

    private async Task<bool> ValidateUser(string emailId, CancellationToken cancellation)
    {
        return await context.Set<EveryEngUser>().AnyAsync(u => u.Email == emailId, cancellation);
    }
}