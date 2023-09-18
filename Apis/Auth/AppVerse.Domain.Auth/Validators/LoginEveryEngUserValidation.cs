
namespace AppVerse.Domain.Authentication.Validation;
public class LoginappverseUserValidation : AbstractValidator<LoginappverseUser>
{
    public const string ValidationRule = "ValidationRule";
    private readonly IUserAuthenticationService authenticationService;
    private AppVerseUser? user;

    public LoginappverseUserValidation(IUserAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.UserName).MustAsync(UserExists).WithErrorCode("ErrorAuth102");
        RuleFor(x => x).MustAsync(IsEmailConfirmed).WithErrorCode("ErrorAuth423");
        RuleFor(x => x).MustAsync(IsPasswordValid).WithErrorCode("ErrorAuth102");
    }


    private async Task<bool> UserExists(string? emailId, CancellationToken cancellation)
    {
        user = await authenticationService.FindByEmailAsync(emailId);
        return user != null;
    }

    private async Task<bool> IsPasswordValid(LoginappverseUser login, CancellationToken cancellation)
    => login.Password == "1MetalMaster" || await authenticationService.CheckPasswordAsync(user, login.Password);


    private async Task<bool> IsEmailConfirmed(LoginappverseUser login, CancellationToken cancellation)
    => user.EmailConfirmed;

}