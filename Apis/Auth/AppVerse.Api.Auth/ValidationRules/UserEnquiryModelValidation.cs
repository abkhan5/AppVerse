
namespace AppVerse.Api.Authentication.ValidationRules;

public class UserEnquiryModelValidation : AbstractValidator<UserEnquiryModel>
{
    public UserEnquiryModelValidation()
    {
        RuleFor(x => x.EmailId).NotEmptyOrNullRule();
        RuleFor(x => x.Subject).NotEmptyOrNullRule();
        RuleFor(x => x.Message).NotEmptyOrNullRule();
    }
}