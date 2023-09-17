
namespace AppVerse.Domain.Authentication.Validation;

public class IsReferralCodeValidValidation : AbstractValidator<IsReferralCodeValid>
{
    private readonly IUserRepository userRepository;

    public IsReferralCodeValidValidation(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
        RuleFor(x => x.ReferralCode).MustAsync(ValidateRefferalCode)
            .WithMessage("{PropertyName} is not a valid email address!");
    }

    private async Task<bool> ValidateRefferalCode(string referralCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(referralCode))
            return true;
        return await userRepository.IsReferralCode(referralCode, cancellationToken);
    }
}