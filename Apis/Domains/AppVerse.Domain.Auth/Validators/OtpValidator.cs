

namespace AppVerse.Domain.Authentication.Validation;

public class OtpValidator : AbstractValidator<ValidateOtp>
{
    private readonly DbContext context;
    private readonly IDistributedCacheRepository distributedCacheRepository;
    public OtpValidator(DbContext context, IDistributedCacheRepository distributedCacheRepository)
    {
        this.context = context;
        this.distributedCacheRepository = distributedCacheRepository;
        RuleFor(x => x.PhoneNumber).MustAsync(UserExists).WithErrorCode(EveryEngErrorRegistry.ErrorAuth102);
        RuleFor(x => x).MustAsync(RequestKeyIsActive).WithErrorCode(EveryEngErrorRegistry.ErrorAuth102);
    }

    private async Task<bool> UserExists(string phoneNumber, CancellationToken cancellationToken)
    => await context.Set<EveryEngUser>().AnyAsync(item => item.PhoneNumber == phoneNumber, cancellationToken);

    private async Task<bool> RequestKeyIsActive(ValidateOtp phoneNumber, CancellationToken cancellationToken)
    {
        var expectedRequestKey = await distributedCacheRepository.GetAsync<string>(phoneNumber.PhoneNumber, cancellationToken);
        return expectedRequestKey != null && expectedRequestKey == phoneNumber.RequestKey;
    }
}
