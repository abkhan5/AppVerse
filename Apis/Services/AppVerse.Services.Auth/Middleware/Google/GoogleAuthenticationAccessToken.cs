namespace AppVerse.Service.Authentication.Middleware.Google;

public class GoogleAuthenticationAccessToken
{
    public string access_token { get; set; }
    public long expires_in { get; set; }
}