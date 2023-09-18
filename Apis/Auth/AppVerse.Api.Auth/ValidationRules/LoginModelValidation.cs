using FluentValidation;

namespace AppVerse.Api.Authentication.ValidationRules;

public class LoginModelValidation : AbstractValidator<LoginModel>
{
    public LoginModelValidation()
    {
        RuleFor(x => x.UserName).NotEmptyOrNullRule();
        RuleFor(x => x.UserName).EmailAddress().WithMessage("{PropertyName} is not a valid email address!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("{PropertyName} should be not empty!");
    }
}