namespace AppVerse.Service.Authentication.Middleware.LinkedIn;

public class LinkedInAuthenticationOptions
{
    public const string OptionName = "LinkedinOptions";
    public string TokenEndpoint { get; set; }
    public string GrantType { get; set; }
    public string RedirectUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ContentType { get; set; }
    public string UserInformationEndpoint { get; set; }
    public string EmailAddressEndpoint { get; set; }
}