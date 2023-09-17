
namespace AppVerse.Service.Realtime;

internal class RealTimeService : IRealTimeService
{
    private HubConnection? connection;
    private readonly IOptions<SignalrSettings> signalrSettings;
    private readonly IIdentityService identityService;
    private bool isStarted;
    private readonly ILogger logger;

    public RealTimeService(ILogger<RealTimeService> logger, IOptions<SignalrSettings> signalrSettings, IIdentityService identityService)
    {
        this.signalrSettings = signalrSettings;
        this.identityService = identityService;
        this.logger = logger;
        var server = $"{signalrSettings.Value.HubConnectionString}/{signalrSettings.Value.HubName}";
        logger.LogTrace("Server url {server}", server);
    }
    async Task IRealTimeService.Sender(string hubMethod, object?[] payload, CancellationToken cancellationToken)
    {
        try
        {

            await connection.InvokeCoreAsync(hubMethod, payload, cancellationToken);
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public async Task Start(CancellationToken cancellationToken)
    {
        try
        {
            if (isStarted)
                return;
            isStarted = true;
            await connection.StartAsync(cancellationToken);

        }
        catch (Exception e)
        {

            throw;
        }
    }

    void IRealTimeService.Receiver<T>(string clientReceiver, Func<T, Task> receiver)
    {
        try
        {

            connection.On<T>(clientReceiver, async message =>
            {
                await receiver(message);
            });
        }
        catch (Exception e)
        {

            throw;
        }
    }
    public void Initialize()
    {
        var server = $"{signalrSettings.Value.HubConnectionString}/{signalrSettings.Value.HubName}";
        logger.LogTrace("Server url {server}", server);
        Debug.WriteLine(server);
        var token = identityService.GetAuthToken();
        if (token == null)
            connection = new HubConnectionBuilder().WithUrl(server,
                options =>
                {
                    options.AccessTokenProvider = async () =>
                    {
                        return token;
                    };
                }).
                AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
                }).WithAutomaticReconnect().Build();
        else
            connection = new HubConnectionBuilder().WithUrl(server).AddJsonProtocol(options =>
            {
                options.PayloadSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
            }).WithAutomaticReconnect().Build();
    }

}