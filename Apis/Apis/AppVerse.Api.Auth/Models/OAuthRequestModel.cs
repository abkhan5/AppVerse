using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public class OAuthRequestModel
{
    public IEnumerable<string> ProfilePictureUrls { get; set; }

    [Required][EmailAddress] public string Email { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}