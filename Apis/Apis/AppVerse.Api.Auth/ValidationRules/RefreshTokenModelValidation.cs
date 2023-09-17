using FluentValidation;

namespace AppVerse.Api.Authentication.ValidationRules;

public class RefreshTokenModelValidation : AbstractValidator<RefreshTokenModel>
{
    public RefreshTokenModelValidation()
    {
        RuleFor(x => x.RefreshToken).NotEmptyOrNullRule().WithErrorCode(EveryEngErrorRegistry.ErrorAuth106);
    }
}