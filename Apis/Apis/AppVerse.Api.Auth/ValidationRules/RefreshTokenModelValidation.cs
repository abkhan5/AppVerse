
namespace AppVerse.Api.Authentication.ValidationRules;

public class RefreshTokenModelValidation : AbstractValidator<RefreshTokenModel>
{
    public RefreshTokenModelValidation()
    {
        RuleFor(x => x.RefreshToken).NotEmptyOrNullRule().WithErrorCode(AppVerseErrorRegistry.ErrorAuth106);
    }
}