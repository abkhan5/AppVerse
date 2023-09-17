namespace AppVerse.Api.Authentication.Middleware.MicrosoftAuth;

public class Office365Options
{
    public string RedirectUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}