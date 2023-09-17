

namespace AppVerse.Api.Authentication.Middleware.LinkedIn;

public static class OAuthMiddlewareExtention
{
    private const string OptionsVerb = "OPTIONS";
    private static readonly string OauthAccessTokenPath = "/api/oauth/accesstoken";

    public static void UseOAuthMiddleware(this IApplicationBuilder builder)
    {
        StringValues identityProvider;

        builder.UseWhen(context =>
                context.Request.Path.HasValue
                && context.Request.Method != OptionsVerb
                && context.Request.Path.Value.Equals(OauthAccessTokenPath,
                    StringComparison.InvariantCultureIgnoreCase)
                && context.Request.Headers.TryGetValue(OAuthConstants.AuthProviderHeaderKey, out identityProvider)
                && identityProvider.Count == 1
                && identityProvider.First() == OAuthConstants.LinkedInProviderName
            , appBuilder => appBuilder.UseMiddleware<LinkedInAuthenticationHandler>());


        builder.UseWhen(context =>
                context.Request.Path.HasValue
                && context.Request.Method != OptionsVerb
                && context.Request.Path.Value.Equals(OauthAccessTokenPath,
                    StringComparison.InvariantCultureIgnoreCase)
                && context.Request.Headers.TryGetValue(OAuthConstants.AuthProviderHeaderKey, out identityProvider)
                && identityProvider.Count == 1
                && identityProvider.First() == OAuthConstants.GoogleProviderName
            , appBuilder => appBuilder.UseMiddleware<GoogleAuthenticationHandler>());
    }
}