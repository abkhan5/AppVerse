namespace AppVerse.Api.Authentication.Middleware.LinkedIn;

public class LinkedInAuthenticationAccessToken
{
    public string access_token { get; set; }
    public long expires_in { get; set; }
}