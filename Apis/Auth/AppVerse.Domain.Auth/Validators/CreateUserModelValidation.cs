
namespace AppVerse.Domain.Authentication.Validation;

public class CreateUserModelValidation : AbstractValidator<CreateProfile>
{
    private readonly IUserAuthenticationService authenticationService;
    public CreateUserModelValidation(IUserAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
        RuleFor(x => x.EmailId).MustAsync(ValidateUser).WithErrorCode("400");
    }

    private async Task<bool> ValidateUser(string emailId, CancellationToken cancellationToken)
    => await authenticationService.FindByNameAsync(emailId) == null;
}