
namespace AppVerse.Api.Authentication.Middleware.Google;

public class GoogleAuthenticationHandler
{
    private readonly IHttpClientFactory clientFactory;
    private readonly IOptions<GoogleAuthenticationOptions> googleOptions;
    private readonly ILogger logger;
    private readonly RequestDelegate next;

    public GoogleAuthenticationHandler(RequestDelegate next,
        ILogger<GoogleAuthenticationHandler> logger,
        IHttpClientFactory clientFactory,
        IOptions<GoogleAuthenticationOptions> googleOptions)
    {
        this.next = next;
        this.clientFactory = clientFactory;
        this.googleOptions = googleOptions;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var hasAuthCode =
            context.Request.Headers.TryGetValue(OAuthConstants.AuthCodeHeaderKey, out var authorizationCode);
        if (!hasAuthCode)
        {
            logger.LogError($"OAUTH {OAuthConstants.GoogleProviderName}: {OAuthConstants.AuthCodeHeaderKey} is empty");
            return;
        }

        logger.LogInformation(
            $"{OAuthConstants.GoogleProviderName}: TokenEndpoint: {googleOptions.Value.TokenEndpoint}");
        logger.LogInformation(
            $"{OAuthConstants.GoogleProviderName}: {OAuthConstants.AuthCodeHeaderKey}: {authorizationCode}");
        if (authorizationCode.Count == 1)
            try
            {
                using var accessTokenRequest =
                    new HttpRequestMessage(HttpMethod.Post, googleOptions.Value.TokenEndpoint)
                    {
                        Content = new FormUrlEncodedContent(InitParamsForTokenRequest(authorizationCode))
                    };

                var httpClient = clientFactory.CreateClient();
                using var accessTokenResponse = await httpClient.SendAsync(accessTokenRequest);
                if (accessTokenResponse.IsSuccessStatusCode)
                {
                    var googleAccessToken = await GetGoogleAccessToken(accessTokenResponse);
                    var googleUser = await GetUserDetailsAsync(googleAccessToken.access_token);

                    var oAuthUser = new OAuthRequestModel
                    {
                        Email = googleUser.EmailId,
                        FirstName = googleUser.FirstName,
                        LastName = googleUser.LastName,
                        ProfilePictureUrls = new[] { googleUser.ProfileUrl }
                    };

                    var requestContent = new StringContent(JsonSerializer.Serialize(oAuthUser), Encoding.UTF8,
                        "application/json");
                    context.Request.Body = await requestContent.ReadAsStreamAsync();
                }
                else
                {
                    var content = await accessTokenResponse.Content.ReadAsStringAsync();
                    logger.LogError($"{OAuthConstants.GoogleProviderName}: Content: {content}");
                    logger.LogError(
                        $"{OAuthConstants.GoogleProviderName}: ReasonPhrase: {accessTokenResponse.ReasonPhrase}");
                    logger.LogError(
                        $"{OAuthConstants.GoogleProviderName}: StatusCode: {accessTokenResponse.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"{OAuthConstants.GoogleProviderName}: Error while loging: {ex.Message}");
                throw;
            }

        await next(context);
    }

    private static async Task<GoogleAuthenticationAccessToken> GetGoogleAccessToken(
        HttpResponseMessage accessTokenResponse)
    {
        var accessTokenJson = await accessTokenResponse.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GoogleAuthenticationAccessToken>(accessTokenJson);
    }

    private async Task<GoogleAuthenticationUser> GetUserDetailsAsync(string accessToken)
    {
        var googleUser = new GoogleAuthenticationUser();
        var requestUri = googleOptions.Value.UserInformationEndpoint;

        using var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
        userInfoRequest.Headers.Add("Authorization", $"Bearer {accessToken}");

        using var userInfoResponse = clientFactory.CreateClient().SendAsync(userInfoRequest);

        await Task.WhenAll(userInfoResponse);

        if (!userInfoResponse.Result.IsSuccessStatusCode)
            return googleUser;

        var userInfoJsonString = await userInfoResponse.Result.Content.ReadAsStringAsync();
        googleUser = JsonSerializer.Deserialize<GoogleAuthenticationUser>(userInfoJsonString);
        return googleUser;
    }

    private Dictionary<string, string> InitParamsForTokenRequest(StringValues authorizationCode)
    {
        var requestToken = new Dictionary<string, string>
        {
            ["code"] = authorizationCode,
            ["grant_type"] = googleOptions.Value.GrantType,
            ["redirect_uri"] = googleOptions.Value.RedirectUri,
            ["client_id"] = googleOptions.Value.ClientId,
            ["client_secret"] = googleOptions.Value.ClientSecret
        };
        logger.LogInformation(
            $"{OAuthConstants.GoogleProviderName}:AUTHCODE: {authorizationCode} , GRANT: {googleOptions.Value.GrantType} ,  Redirect: {googleOptions.Value.RedirectUri} ");
        return requestToken;
    }
}