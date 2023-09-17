
namespace AppVerse.Domain.Authentication.Validation;

public class IsIdValidValidation : AbstractValidator<IsIdValid>
{
    private readonly DbContext context;
    public IsIdValidValidation(DbContext context)
    {
        this.context = context;
        RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("Can not be empty!");
        RuleFor(x => x.UserId).Must(IsValidInput).WithMessage("Email is not a valid email address!");
        RuleFor(x => x.UserId).MustAsync(UserExists).WithErrorCode(EveryEngErrorRegistry.ErrorAuth103);
    }
    private async Task<bool> UserExists(string userLoginInput, CancellationToken cancellationToken)
    => !await context.Set<EveryEngUser>().AnyAsync(item => item.PhoneNumber == userLoginInput || item.Email == userLoginInput, cancellationToken);


    private bool IsValidInput(string inputString)
    {
        if (UInt128.TryParse(inputString, out UInt128 _))
            return true;

        if (string.IsNullOrEmpty(inputString))
            return false;
        var isEmail = Regex.IsMatch(inputString,
            @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            RegexOptions.IgnoreCase);
        return isEmail;
    }

}
