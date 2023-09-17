#region Namespace


#endregion


namespace AppVerse.Service.Authentication;

internal class ProfileClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppVerseUser>
{
    private readonly IDistributedCacheRepository distributedCacheRepository;
    public ProfileClaimsPrincipalFactory(UserManager<AppVerseUser> userManager, IOptions<IdentityOptions> optionsAccessor, IDistributedCacheRepository distributedCacheRepository)
        : base(userManager, optionsAccessor)
    {
        this.distributedCacheRepository = distributedCacheRepository;
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppVerseUser user)
    {
        var recordId = user.Id;
        var identity = await base.GenerateClaimsAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Email),
            new(JwtRegisteredClaimNames.Jti, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.AuthTime, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMilliseconds(1).ToString(CultureInfo.InvariantCulture)),
        };

        if (!string.IsNullOrEmpty(recordId))
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, recordId));
        identity.AddClaims(claims);


        await distributedCacheRepository.SetLocalEntityAsync(AppVerseUserDto.CreateCacheKey(user.UserName), new AppVerseUserDto(user), new TimeSpan(1, 0, 0), CancellationToken.None);
        return identity;
    }
}