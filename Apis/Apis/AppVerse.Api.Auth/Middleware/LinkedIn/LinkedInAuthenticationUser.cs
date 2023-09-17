namespace AppVerse.Api.Authentication.Middleware.LinkedIn;

public class LinkedInAuthenticationUser
{
    public IEnumerable<string> ProfilePictureUrls { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}