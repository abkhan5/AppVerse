

namespace AppVerse.Domain.Authentication.Validation;

public class OnRefreshTokenValidation : AbstractValidator<OnRefreshToken>
{
    private readonly DbContext context;

    public OnRefreshTokenValidation(DbContext context)
    {
        this.context = context;
        RuleFor(x => x.RefreshToken).MustAsync(ValidateToken).WithErrorCode(AppVerseErrorRegistry.ErrorAuth106);
        RuleFor(x => x.RefreshToken).MustAsync(ValidateUser).WithErrorCode(AppVerseErrorRegistry.ErrorAuth101);
    }


    private async Task<bool> ValidateToken(string refreshToken, CancellationToken cancellation)
    {
        var isValid = true;
        var token = await context.Set<RefreshToken>().Include(item => item.Profile)
            .FirstAsync(u => u.Token == refreshToken, cancellation);
        if (token == null)
            isValid = false;

        if (token.Invalidated)
            isValid = false;

        return isValid;
    }

    private async Task<bool> ValidateUser(string refreshToken, CancellationToken cancellation)
    {
        var isValid = true;
        var token = await context.Set<RefreshToken>().Include(item => item.Profile)
            .FirstAsync(u => u.Token == refreshToken, cancellation);

        if (token.Profile == null)
            isValid = false;

        return isValid;
    }
}