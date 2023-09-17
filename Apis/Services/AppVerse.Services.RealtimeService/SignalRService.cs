using AppVerse.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppVerse.Services.RealtimeService;

public class SignalRService : IRealTimeService
{
    private const string HubName = "AppVerseUser";

    protected readonly IOptions<AppServiceSettings> appverseServiceSettings;

    //private string hubName;
    private readonly ILogger logger;
    protected HubConnection connection;
    private bool isInitialized;

    public SignalRService(IOptions<AppServiceSettings> appverseServiceSettings, ILogger<SignalRService> logger)
    {
        this.appverseServiceSettings = appverseServiceSettings;
        this.logger = logger;
    }


    async Task IRealTimeService.SendMessage(string hubMethod, object?[] payload, CancellationToken cancellationToken)
    {
        try
        {
            if (!isInitialized)
                await Initialized(cancellationToken);
            await connection.InvokeCoreAsync(hubMethod, payload, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }

    private async Task<HubConnection> GetHubConnectionBuilder(string server, CancellationToken stoppingToken) =>
        new HubConnectionBuilder().WithUrl(server,
            options =>
            {
            })
        .WithAutomaticReconnect()
        .AddMessagePackProtocol()
        .Build();


    private async Task Initialized(CancellationToken stoppingToken)
    {
        try
        {
            var server = $"{appverseServiceSettings.Value.MessengerHubServiceHost}/{HubName}";
            //var server = $"https://localhost:45210/{HubName}";
            connection = await GetHubConnectionBuilder(server, stoppingToken);
            connection.Closed += OnConnection_Closed;
            connection.Reconnected += OnConnection_Reconnected;
            connection.Reconnecting += OnConnection_Reconnecting;
            await connection.StartAsync(stoppingToken);
            isInitialized = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

    protected async Task OnConnection_Reconnecting(Exception? arg)
    {
        logger.LogError(arg, "");
    }

    protected async Task OnConnection_Reconnected(string? arg)
    {
        logger.LogInformation(arg, "");
    }

    protected async Task OnConnection_Closed(Exception? arg)
    {
        logger.LogError(arg, "");
    }

    public async Task StreamMessages<TEntity>(string hubMethod, Func<IAsyncEnumerable<TEntity>> stream, CancellationToken cancellationToken)
    {
        try
        {
            if (!isInitialized)
                await Initialized(cancellationToken);
            await connection.SendAsync(hubMethod, stream(), cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }


}