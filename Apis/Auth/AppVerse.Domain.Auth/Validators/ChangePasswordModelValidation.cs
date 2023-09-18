namespace AppVerse.Domain.Authentication.Validation;

public class ChangePasswordModelValidation : AbstractValidator<ChangePassword>
{
    private readonly DbContext context;
    private readonly IIdentityService identityService;

    public ChangePasswordModelValidation(DbContext context, IIdentityService identityService)
    {
        this.context = context;
        this.identityService = identityService;

        RuleFor(x => x).MustAsync(ValidateUser).WithErrorCode(AppVerseErrorRegistry.ErrorAuth101);
        RuleFor(x => x.Password).NotEmptyOrNullRule();
        RuleFor(x => x.UpdatedPassword).NotEmptyOrNullRule();
        //RuleFor(x => x).Must(x => x.Password != x.UpdatedPassword).NotNull().WithMessage(appverseErrorRegistry.ErrorAuth107);
    }

    private async Task<bool> ValidateUser(ChangePassword arg1, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();

        return await context.Set<AppVerseUser>().AnyAsync(u => u.Id == userId, cancellationToken);
    }
}