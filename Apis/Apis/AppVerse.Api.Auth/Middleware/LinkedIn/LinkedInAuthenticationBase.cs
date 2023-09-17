using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;

namespace AppVerse.Api.Authentication.Middleware.LinkedIn;

public class LinkedInAuthenticationBase
{
    protected ISet<string> Fields { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        ProfileFields.Id,
        ProfileFields.FirstName,
        ProfileFields.LastName,
        ProfileFields.PictureUrl
    };

    protected virtual string GetEmail([NotNull] JsonElement jsonElement)
    {
        if (!jsonElement.TryGetProperty("elements", out var emails)) return null;

        return emails
            .EnumerateArray()
            .Select(p => p.GetProperty("handle~"))
            .Select(p => p.GetString("emailAddress"))
            .FirstOrDefault();
    }

    protected static IEnumerable<string> GetPictureUrls(JsonElement user)
    {
        if (!user.TryGetProperty("profilePicture", out var profilePicture) ||
            !profilePicture.TryGetProperty("displayImage~", out var displayImage) ||
            !displayImage.TryGetProperty("elements", out var displayImageElements))
            return Array.Empty<string>();

        var pictureUrls = new List<string>();

        foreach (var element in displayImageElements.EnumerateArray())
        {
            if (!string.Equals(element.GetString("authorizationMethod"), "PUBLIC", StringComparison.Ordinal) ||
                !element.TryGetProperty("identifiers", out var imageIdentifier))
                continue;

            var pictureUrl = imageIdentifier
                .EnumerateArray()
                .FirstOrDefault()
                .GetString("identifier");

            if (!string.IsNullOrEmpty(pictureUrl)) pictureUrls.Add(pictureUrl);
        }

        return pictureUrls;
    }

    protected string GetMultiLocaleString(JsonElement user, string propertyName)
    {
        if (!user.TryGetProperty(propertyName, out var property)) return null;

        if (!property.TryGetProperty("localized", out var propertyLocalized)) return null;

        string preferredLocaleKey = null;

        if (property.TryGetProperty("preferredLocale", out var preferredLocale))
            preferredLocaleKey = $"{preferredLocale.GetString("language")}_{preferredLocale.GetString("country")}";

        var preferredLocales = new Dictionary<string, string>();

        foreach (var element in propertyLocalized.EnumerateObject())
            preferredLocales[element.Name] = element.Value.GetString();

        return MultiLocaleStringResolver(preferredLocales, preferredLocaleKey);
    }

    private static string MultiLocaleStringResolver(IReadOnlyDictionary<string, string> localizedValues,
        string preferredLocale)
    {
        if (!string.IsNullOrEmpty(preferredLocale) &&
            localizedValues.TryGetValue(preferredLocale, out var preferredLocaleValue))
            return preferredLocaleValue;

        var currentUIKey = Thread.CurrentThread.CurrentUICulture.ToString().Replace('-', '_');
        if (localizedValues.TryGetValue(currentUIKey, out var currentUIValue)) return currentUIValue;

        return localizedValues.Values.FirstOrDefault();
    }

    protected static class ProfileFields
    {
        public const string Id = "id";
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
        public const string PictureUrl = "profilePicture(displayImage~:playableStreams)";
    }
}