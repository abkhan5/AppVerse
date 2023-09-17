using FluentValidation;

namespace AppVerse.Api.Authentication.ValidationRules;

public class ResetPasswordModelValidation : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidation()
    {
        RuleFor(x => x.Password).PasswordRule();
        RuleFor(x => x).Must(x => x.Password == x.ConfirmPassword)
            .WithMessage(x => $"{x.Password} Password should be the same");
        RuleFor(x => x.ConfirmPassword).PasswordRule();
    }
}