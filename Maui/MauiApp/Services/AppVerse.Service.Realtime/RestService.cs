namespace AppVerse.Service.Realtime;

public class RestService : IRestService
{

    //private readonly IConnectivity connectivity;
    private readonly IHttpClientFactory? httpClientFactory;
    private readonly IIdentityService? identityService;
    private string clientName;
    public RestService(IHttpClientFactory httpClientFactory, IIdentityService identityService)
    {
        this.httpClientFactory = httpClientFactory;
        this.identityService = identityService;
    }
    protected HttpClient GetClient()
    {
        var httpClient = httpClientFactory.CreateClient(clientName);
        httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(identityService.GetAuthToken());
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        return httpClient;

    }
    public void Initialize(string serviceName)
    {
        clientName = serviceName;
    }

    public async Task<T> Get<T>(string resource, int cacheDuration, CancellationToken cancellationToken)
    {
        var httpClient = GetClient();
        return await httpClient.GetFromJsonAsync<T>(new Uri(httpClient.BaseAddress, resource), cancellationToken);
    }

    public async Task Post<T>(string uri, T payload)
    {
        var dataToPost = JsonSerializer.Serialize(payload);
        var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");
        var httpClient = GetClient();
        var response = await httpClient.PostAsync(new Uri(httpClient.BaseAddress, uri), content);
        response.EnsureSuccessStatusCode();
    }

    public async Task Put<T>(string uri, T payload)
    {
        var dataToPost = JsonSerializer.Serialize(payload);
        var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");
        var httpClient = GetClient();
        var response = await httpClient.PutAsync(new Uri(httpClient.BaseAddress, uri), content);
        response.EnsureSuccessStatusCode();
    }

    public async Task Delete(string uri)
    {
        var httpClient = GetClient();
        HttpResponseMessage response = await httpClient.DeleteAsync(new Uri(httpClient.BaseAddress, uri));
        response.EnsureSuccessStatusCode();
    }
}
