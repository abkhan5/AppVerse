using System.Text.Json.Serialization;

namespace AppVerse.Api.Authentication.Middleware.Google;

public class GoogleAuthenticationUser
{
    [JsonPropertyName("name")] public string Name { get; set; }

    [JsonPropertyName("given_name")] public string FirstName { get; set; }

    [JsonPropertyName("family_name")] public string LastName { get; set; }

    [JsonPropertyName("picture")] public string ProfileUrl { get; set; }

    [JsonPropertyName("email")] public string EmailId { get; set; }
}