
namespace AppVerse.Service.Authentication;

internal class AppUserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserClaimsPrincipalFactory<AppVerseUser> claimsPrincipalFactory;
    private readonly IDistributedCacheRepository distributedCacheRepository;
    private readonly IOptions<AppServiceSettings> appverseServiceOption;
    private readonly JwtTokenGenerator jwtTokenGenerator;

    private readonly IUniqueCodeGeneratorService uniqueCodeGeneratorService;
    private readonly UserManager<AppVerseUser> userManager;

    public AppUserAuthenticationService(
        UserManager<AppVerseUser> userManager,
        IUserClaimsPrincipalFactory<AppVerseUser> claimsPrincipalFactory,
        JwtTokenGenerator jwtTokenGenerator,
        IOptions<AppServiceSettings> appverseServiceOption,
        IDistributedCacheRepository distributedCacheRepository,
        IUniqueCodeGeneratorService uniqueCodeGeneratorService)
    {
        this.userManager = userManager;
        this.claimsPrincipalFactory = claimsPrincipalFactory;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.appverseServiceOption = appverseServiceOption;
        this.distributedCacheRepository = distributedCacheRepository;
        this.uniqueCodeGeneratorService = uniqueCodeGeneratorService;
    }

    public async Task<string> GenerateSignupLink(AppVerseUser user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var appverseWebUrl = appverseServiceOption.Value.ServiceHost;
        var confirmUrl = $"{appverseWebUrl}/{appverseServiceOption.Value.VerifyAccountRoute}/"
                         + $"?token={HttpUtility.UrlEncode(token)}"
                         + $"&email={HttpUtility.UrlEncode(user.Email)}";
        return confirmUrl;
    }

    public async Task<string> GetLoginLinkAsync()
    => $"{appverseServiceOption.Value.ServiceHost}/auth/login";


    public async Task<EmailConfirmationDto> GetConfirmationModel(AppVerseUser user)
    => new EmailConfirmationDto
    {
        Token = await userManager.GenerateEmailConfirmationTokenAsync(user),
        Address = appverseServiceOption.Value.AuthServiceHost,
        UserEmail = user.Email,
        Path = appverseServiceOption.Value.ConfirmationEmailPath
    };


    public async Task<string> GeneratePasswordResetTokenAsync(AppVerseUser user)
    {
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var appverseWebUrl = appverseServiceOption.Value.ServiceHost;
        var confirmUrl = $"{appverseWebUrl}/{appverseServiceOption.Value.ResetPasswordRoute}/"
                         + $"?token={HttpUtility.UrlEncode(token)}"
                         + $"&email={HttpUtility.UrlEncode(user.Email)}";
        return confirmUrl;
    }


    public async Task<IdentityResult> AccessFailedAsync(AppVerseUser user)
    => await userManager.AccessFailedAsync(user);



    public async Task<bool> IsEmailConfirmedAsync(AppVerseUser user)
    => await userManager.IsEmailConfirmedAsync(user);


    public async Task ResetAccessFailedCountAsync(AppVerseUser user)
    => await userManager.ResetAccessFailedCountAsync(user);


    public async Task<ClaimsPrincipal> CreatePrincipalAsync(AppVerseUser user)
    => await claimsPrincipalFactory.CreateAsync(user);



    public async Task<AppVerseUser> FindByEmailAsync(string emailId)
    => await userManager.FindByEmailAsync(emailId);



    public async Task<AppVerseUser> FindByIdAsync(string emailId)
    => await userManager.FindByIdAsync(emailId);

    public async Task<AppVerseUser> FindByNameAsync(string emailId)
    => await userManager.FindByEmailAsync(emailId);

    public async Task<IdentityResult> ChangePasswordAsync(AppVerseUser user, string token, string newPassword)
    => await userManager.ChangePasswordAsync(user, token, newPassword);

    public async Task<IdentityResult> ResetPasswordAsync(AppVerseUser user, string token, string newPassword)
    => await userManager.ResetPasswordAsync(user, token, newPassword);

    public async Task<IdentityResult> ConfirmEmailAsync(AppVerseUser user, string password)
    => await userManager.ConfirmEmailAsync(user, password);

    public async Task<IdentityResult> CreateAsync(AppVerseUser user, string password)
    => await userManager.CreateAsync(user, password);

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppVerseUser user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var appverseWebUrl = appverseServiceOption.Value.ServiceHost;
        var confirmUrl = $"{appverseWebUrl}/{appverseServiceOption.Value.VerifyAccountRoute}/"
                         + $"?token={HttpUtility.UrlEncode(token)}"
                         + $"&email={HttpUtility.UrlEncode(user.Email)}";
        return confirmUrl;
    }


    public async Task<IdentityResult> CreateAsync(AppVerseUser user)
    => await userManager.CreateAsync(user);

    public async Task<AuthenticationResponseModel> GenerateToken(ClaimsPrincipal principal, AppVerseUser user, CancellationToken cancellationToken)
    {
        var response = await jwtTokenGenerator.GenerateToken(principal, user.Id, cancellationToken);
        return new AuthenticationResponseModel
        {
            IsProfileActive = user.LockoutEnabled,
            RefreshToken = response.RefreshToken,
            Token = response.Token,
            ProfileId = user.Id
        };
    }

    public async Task<bool> CheckPasswordAsync(AppVerseUser user, string password)
    => await userManager.CheckPasswordAsync(user, password);

    public async Task<bool> IsCreationTokenValid(string emailId, string token, CancellationToken cancellationToken)
    {
        var actualToken = await distributedCacheRepository.GetAsync<CreateUserToken>(emailId, cancellationToken);
        if (string.IsNullOrEmpty(token) || actualToken == null || string.IsNullOrEmpty(actualToken.Token))
            return false;

        return actualToken.Token == token;
    }

    public async Task<string> CreateToken(string userId, CancellationToken cancellationToken)
    {
        var creationToken =
            uniqueCodeGeneratorService.GetUniqueCode($"{userId}~{DateTime.UtcNow.ToShortTimeString()}");
        await distributedCacheRepository.SetEntityAsync(userId,
            new CreateUserToken { Token = creationToken, UserId = userId }, new TimeSpan(0, 15, 0),
            cancellationToken);
        return creationToken;
    }

    private record CreateUserToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}