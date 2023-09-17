using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace AppVerse.Service.MsOffice365;


public class OfficeClientService
{
    private readonly IOptions<OfficeSettings> azureSettings;
    private GraphServiceClient? graphServiceClient;
    public OfficeClientService(IOptions<OfficeSettings> azureSettings)
    {
        this.azureSettings = azureSettings;
    }

    public  GraphServiceClient GetGraphClient()
    {
        if (graphServiceClient != null)
            return graphServiceClient;
        string[] scopes = new[] { "https://graph.microsoft.com/.default" };
        var tenantId = azureSettings.Value.TenantId;            

        // Values from app registration
        var clientId = azureSettings.Value.ClientId;
        var clientSecret = azureSettings.Value.ClientSecret;

        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        // https://docs.microsoft.com/dotnet/api/azure.identity.clientsecretcredential
        var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);

        graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);

        return graphServiceClient;
    }


    public async Task<string> GetUserIdAsync()
    {
        var meetingOrganizer = azureSettings.Value.MeetingOrganizer;
        var filter = $"startswith(userPrincipalName,'{meetingOrganizer}')";
        var graphServiceClient = GetGraphClient();

        var users = await graphServiceClient.Users
            .Request()
            .Filter(filter)
            .GetAsync();

        return users.CurrentPage[0].Id;
    }
}