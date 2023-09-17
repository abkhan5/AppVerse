
namespace AppVerse.Domain.Authentication.Validation;

public class CreateUserModelValidation : AbstractValidator<CreateProfile>
{
    private readonly IUserAuthenticationService authenticationService;
    public CreateUserModelValidation(IUserAuthenticationService authenticationService, IEveryEngAuthorizedUserEventStore cmsUserStore)
    {
        this.authenticationService = authenticationService;
        RuleFor(x => x.EmailId).MustAsync(ValidateUser).WithErrorCode(EveryEngErrorRegistry.ErrorAuth102);
    }

    private async Task<bool> ValidateUser(string emailId, CancellationToken cancellationToken)
    => await authenticationService.FindByNameAsync(emailId) == null;
}