using FluentValidation;

namespace AppVerse.Api.Authentication.ValidationRules;

public class BookDemoModelValidation : AbstractValidator<BookADemo>
{
    public BookDemoModelValidation()
    {
        RuleFor(x => x).Must(item => item.PhoneNumber.HasValue || !string.IsNullOrEmpty(item.EmailId)).WithMessage("Submit either Phone Number or EmailId");
    }
}