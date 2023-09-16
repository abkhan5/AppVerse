

namespace AppVerse.Service.Authentication;

internal class ProfileUserStore : IUserStore<AppVerseUser>, IUserPasswordStore<AppVerseUser>,
    IUserEmailStore<AppVerseUser>, IUserLockoutStore<AppVerseUser>, IUserTwoFactorStore<AppVerseUser>

{
    private readonly DbContext context;
    private readonly IUniqueCodeGeneratorService uniqueCodeGeneratorService;

    public ProfileUserStore(IUniqueCodeGeneratorService uniqueCodeGeneratorService, DbContext context)
    {
        this.uniqueCodeGeneratorService = uniqueCodeGeneratorService;
        this.context = context;
    }

    public void Dispose()
    {
        context.Dispose();
    }

    #region User store methods

    public async Task<string> GetUserIdAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await context.Set<AppVerseUser>()
            .FirstOrDefaultAsync(profileItem => profileItem.Email == user.Email, cancellationToken);
        if (userProfile == null) return user.Id;
        return userProfile.Id;
    }

    public async Task<string> GetUserNameAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);

        if (userProfile == null)
            return user.UserName;

        return userProfile.UserName;
    }

    public async Task SetUserNameAsync(AppVerseUser user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
    }

    public async Task<string> GetNormalizedUserNameAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        if (userProfile == null)
            return user.NormalizedUserName;

        return userProfile?.NormalizedUserName;
    }

    public async Task SetNormalizedUserNameAsync(AppVerseUser user, string normalizedName,
        CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
    }


    public async Task<IdentityResult> UpdateAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        try
        {
            context.Set<AppVerseUser>().Update(user);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "Error",
                Description = ex.ToString()
            });
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        try
        {
            context.Set<AppVerseUser>().Remove(user);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "Error",
                Description = ex.ToString()
            });
        }

        return IdentityResult.Success;
    }

    public async Task<AppVerseUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var userProfile = await context.Set<AppVerseUser>()
            .FirstOrDefaultAsync(profileItem => profileItem.Id == userId, cancellationToken);
        return userProfile;
    }

    public async Task<AppVerseUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        var userProfile = await context.Set<AppVerseUser>()
            .FirstOrDefaultAsync(profileItem => profileItem.NormalizedUserName == normalizedUserName,
                cancellationToken);
        return userProfile;
    }

    public async Task SetPasswordHashAsync(AppVerseUser user, string passwordHash,
        CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
    }

    public async Task<string> GetPasswordHashAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        if (userProfile == null) return user.PasswordHash;
        return userProfile?.PasswordHash;
    }

    public async Task<bool> HasPasswordAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        if (userProfile == null) return !string.IsNullOrEmpty(user.PasswordHash);
        return !string.IsNullOrEmpty(userProfile.PasswordHash);
    }

    public async Task<IdentityResult> CreateAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        try
        {
            if (user.Id == Guid.Empty.ToString())
                user.Id = Guid.NewGuid().ToString().GetHashCode().ToString("x");

            if (string.IsNullOrEmpty(user.NormalizedUserName))
                user.NormalizedUserName = user.UserName.ToUpper();
            if (string.IsNullOrEmpty(user.NormalizedEmail))
                user.NormalizedEmail = user.Email.ToUpper();

            while (true)
            {
                var referralCode = $"{Guid.NewGuid()}".GetHashCode().ToString("x");
                var isCodeUnique = await context.Set<AppVerseUser>()
                    .AnyAsync(eeItem => eeItem.ReferralCode == referralCode, cancellationToken);
                if (isCodeUnique)
                    continue;
                user.ReferralCode = referralCode;
                break;
            }


            await context.Set<AppVerseUser>().AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "Error",
                Description = ex.ToString()
            });
        }

        return IdentityResult.Success;
    }

    public async Task<AppVerseUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
    {
        var userProfile = await context.Set<AppVerseUser>()
            .FirstOrDefaultAsync(profileItem => profileItem.NormalizedEmail == normalizedEmail, cancellationToken);

        return userProfile;
    }

    public async Task<string> GetEmailAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        if (userProfile == null)
            return user.Email;

        return userProfile.Email;
    }

    public async Task<bool> GetEmailConfirmedAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        if (userProfile == null)
            return false;
        return userProfile.EmailConfirmed;
    }

    public async Task<string> GetNormalizedEmailAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        if (userProfile == null)
            return user.NormalizedEmail;

        return userProfile?.NormalizedEmail;
    }

    public async Task SetEmailAsync(AppVerseUser user, string email, CancellationToken cancellationToken)
    {
        user.Email = email;
    }

    public async Task SetEmailConfirmedAsync(AppVerseUser user, bool confirmed, CancellationToken cancellationToken)
    {
        user.EmailConfirmed = confirmed;
    }

    private async Task<AppVerseUser> GetProfileAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await context.Set<AppVerseUser>()
            .FirstOrDefaultAsync(profileItem => profileItem.Id == user.Id, cancellationToken);
        return userProfile;
    }

    public async Task SetNormalizedEmailAsync(AppVerseUser user, string normalizedEmail,
        CancellationToken cancellationToken)
    {
        user.NormalizedEmail = normalizedEmail;
    }

    #endregion


    #region UserLockouStore

    public async Task<DateTimeOffset?> GetLockoutEndDateAsync(AppVerseUser user,
        CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        return userProfile.LockoutEnd;
    }

    public async Task SetLockoutEndDateAsync(AppVerseUser user, DateTimeOffset? lockoutEnd,
        CancellationToken cancellationToken)
    {
        user.LockoutEnd = lockoutEnd;
    }

    public async Task<int> IncrementAccessFailedCountAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        user.AccessFailedCount++;
        return user.AccessFailedCount;
    }

    public async Task ResetAccessFailedCountAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        user.AccessFailedCount = 0;
        await SaveUserProfile(user, cancellationToken);
    }

    public async Task<int> GetAccessFailedCountAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        return userProfile.AccessFailedCount;
    }

    public async Task<bool> GetLockoutEnabledAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        return userProfile.LockoutEnabled;
    }

    public async Task SetLockoutEnabledAsync(AppVerseUser user, bool enabled, CancellationToken cancellationToken)
    {
        user.LockoutEnabled = enabled;
    }

    private async Task SaveUserProfile(AppVerseUser user, CancellationToken cancellationToken)
    {
        if (await context.Set<AppVerseUser>().AnyAsync(item => item.Id == user.Id, cancellationToken))
        {
            context.Set<AppVerseUser>().Attach(user);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    #endregion

    #region UserTwoFactorStore

    public async Task SetTwoFactorEnabledAsync(AppVerseUser user, bool enabled, CancellationToken cancellationToken)
    {
        user.TwoFactorEnabled = enabled;
    }

    public async Task<bool> GetTwoFactorEnabledAsync(AppVerseUser user, CancellationToken cancellationToken)
    {
        var userProfile = await GetProfileAsync(user, cancellationToken);
        return userProfile.TwoFactorEnabled;
    }

    #endregion
}