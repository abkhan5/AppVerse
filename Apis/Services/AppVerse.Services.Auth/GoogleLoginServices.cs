
namespace AppVerse.Service.Authentication;

public record GoogleLoginCredentialsSettings
{
    public const string GoogleLoginCredentialsOptions = "GoogleLoginCredentials";
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
public record GoogleCredentialsDto
{
    public string EmailId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ProfileImageUrl { get; set; }
}

public class GoogleLoginServices
{
    private readonly SignInManager<AppVerseUser> signInManager;

    public GoogleLoginServices(SignInManager<AppVerseUser> signInManager)
    {
        this.signInManager = signInManager;
    }

    public async Task<GoogleCredentialsDto> ExternalLoginCallback(string returnUrl, string remoteError)
    {
        var info = await signInManager.GetExternalLoginInfoAsync();

        if (info == null)
            return null;
        // Sign in the user with this external login provider if the user already has a login.
        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);
        if (result.Succeeded)
        {
            return null;
            //_logger.LogInformation("User logged in with {Name} provider.", info.LoginProvider);
            //return RedirectToAction(nameof(returnUrl));
        }

        GoogleCredentialsDto googleCredentials = new();
        //get google login user infromation like that.
        if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            googleCredentials.EmailId = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
            googleCredentials.FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
        if (info.Principal.HasClaim(c => c.Type == ClaimTypes.GivenName))
            googleCredentials.LastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
        if (info.Principal.HasClaim(c => c.Type == "picture"))
            googleCredentials.ProfileImageUrl = info.Principal.FindFirstValue("picture");
        return googleCredentials;
    }
}