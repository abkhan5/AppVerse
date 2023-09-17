using Microsoft.AspNetCore.WebUtilities;

namespace AppVerse.Api.Authentication.Middleware.LinkedIn;

public class LinkedInAuthenticationHandler : LinkedInAuthenticationBase
{
    private readonly IHttpClientFactory clientFactory;
    private readonly IOptions<LinkedInAuthenticationOptions> linkedInOptions;
    private readonly ILogger logger;
    private readonly RequestDelegate next;

    public LinkedInAuthenticationHandler(RequestDelegate next,
        ILogger<LinkedInAuthenticationHandler> logger,
        IHttpClientFactory clientFactory,
        IOptions<LinkedInAuthenticationOptions> linkedInOptions)
    {
        this.next = next;
        this.clientFactory = clientFactory;
        this.linkedInOptions = linkedInOptions;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var hasAuthCode =
            context.Request.Headers.TryGetValue(OAuthConstants.AuthCodeHeaderKey, out var authorizationCode);
        if (!hasAuthCode)
        {
            logger.LogError(
                $"OAUTH {OAuthConstants.LinkedInProviderName}: {OAuthConstants.AuthCodeHeaderKey} is empty");
            return;
        }

        var defaultRequestContent = new StringContent(JsonSerializer.Serialize(new LinkedInAuthenticationUser()),
            Encoding.UTF8, "application/json");
        context.Request.Body = await defaultRequestContent.ReadAsStreamAsync();

        logger.LogInformation(
            $"{OAuthConstants.LinkedInProviderName}: TokenEndpoint: {linkedInOptions.Value.TokenEndpoint}");
        logger.LogInformation(
            $"{OAuthConstants.LinkedInProviderName}: {OAuthConstants.AuthCodeHeaderKey}: {authorizationCode}");
        if (authorizationCode.Count == 1)
            try
            {
                using var accessTokenRequest =
                    new HttpRequestMessage(HttpMethod.Post, linkedInOptions.Value.TokenEndpoint)
                    {
                        Content = new FormUrlEncodedContent(InitParamsForTokenRequest(authorizationCode))
                    };

                var httpClient = clientFactory.CreateClient();
                using var accessTokenResponse = await httpClient.SendAsync(accessTokenRequest);
                if (accessTokenResponse.IsSuccessStatusCode)
                {
                    var linkedInAccessToken = await GetLinkedInAccessToken(accessTokenResponse);
                    var linkedInUser = await GetUserDetailsAsync(linkedInAccessToken.access_token);

                    var requestContent = new StringContent(JsonSerializer.Serialize(linkedInUser), Encoding.UTF8,
                        "application/json");
                    context.Request.Body = await requestContent.ReadAsStreamAsync();
                }
                else
                {
                    var content = await accessTokenResponse.Content.ReadAsStringAsync();
                    logger.LogError($"{OAuthConstants.LinkedInProviderName}: Content: {content}");
                    logger.LogError(
                        $"{OAuthConstants.LinkedInProviderName}: ReasonPhrase: {accessTokenResponse.ReasonPhrase}");
                    logger.LogError(
                        $"{OAuthConstants.LinkedInProviderName}: StatusCode: {accessTokenResponse.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"{OAuthConstants.LinkedInProviderName}: Error while loging: {ex.Message}");
                throw;
            }

        await next(context);
    }

    private static async Task<LinkedInAuthenticationAccessToken> GetLinkedInAccessToken(
        HttpResponseMessage accessTokenResponse)
    {
        var accessTokenJson = await accessTokenResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LinkedInAuthenticationAccessToken>(accessTokenJson);
    }

    private async Task<LinkedInAuthenticationUser> GetUserDetailsAsync(string accessToken)
    {
        var linkedInUser = new LinkedInAuthenticationUser();
        var requestUri = linkedInOptions.Value.UserInformationEndpoint;
        requestUri = QueryHelpers.AddQueryString(requestUri, "projection", $"({string.Join(",", Fields)})");

        using var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
        userInfoRequest.Headers.Add("Authorization", $"Bearer {accessToken}");

        using var userEmailRequest =
            new HttpRequestMessage(HttpMethod.Get, linkedInOptions.Value.EmailAddressEndpoint);
        userEmailRequest.Headers.Add("Authorization", $"Bearer {accessToken}");


        using var userInfoResponse = clientFactory.CreateClient().SendAsync(userInfoRequest);
        using var userEmailResponse = clientFactory.CreateClient().SendAsync(userEmailRequest);

        await Task.WhenAll(userInfoResponse, userEmailResponse);

        if (!userInfoResponse.Result.IsSuccessStatusCode || !userEmailResponse.Result.IsSuccessStatusCode)
            return linkedInUser;

        using var profileJsonDoc = JsonDocument.Parse(await userInfoResponse.Result.Content.ReadAsStringAsync());
        using var emailJsonDoc = JsonDocument.Parse(await userEmailResponse.Result.Content.ReadAsStringAsync());

        linkedInUser.FirstName = GetMultiLocaleString(profileJsonDoc.RootElement, ProfileFields.FirstName);
        linkedInUser.LastName = GetMultiLocaleString(profileJsonDoc.RootElement, ProfileFields.LastName);
        linkedInUser.ProfilePictureUrls = GetPictureUrls(profileJsonDoc.RootElement);
        linkedInUser.Email = GetEmail(emailJsonDoc.RootElement);
        return linkedInUser;
    }

    private Dictionary<string, string> InitParamsForTokenRequest(StringValues authorizationCode)
    {
        var requestToken = new Dictionary<string, string>
        {
            ["code"] = authorizationCode,
            ["grant_type"] = linkedInOptions.Value.GrantType,
            ["redirect_uri"] = linkedInOptions.Value.RedirectUri,
            ["client_id"] = linkedInOptions.Value.ClientId,
            ["client_secret"] = linkedInOptions.Value.ClientSecret
        };
        logger.LogInformation(
            $"{OAuthConstants.LinkedInProviderName}:AUTHCODE: {authorizationCode} , GRANT: {linkedInOptions.Value.GrantType} ,  Redirect: {linkedInOptions.Value.RedirectUri} ");
        return requestToken;
    }
}