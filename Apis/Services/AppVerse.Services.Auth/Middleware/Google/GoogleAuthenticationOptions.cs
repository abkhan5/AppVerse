namespace AppVerse.Service.Authentication.Middleware.Google;

public class GoogleAuthenticationOptions
{
    public const string OptionName = "GoogleOptions";
    public string TokenEndpoint { get; set; }
    public string GrantType { get; set; }
    public string RedirectUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string UserInformationEndpoint { get; set; }
}