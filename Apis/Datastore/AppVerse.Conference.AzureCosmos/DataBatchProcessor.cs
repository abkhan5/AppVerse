using AppVerse.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AppVerse.Storage.AzureCosmosDbSql;

internal class DataBatchProcessor : IDataBatchProcessor
{
    private readonly IOptions<AzureCosmosDbSettings> cosmosDbOptions;
    private ChangeFeedProcessor? changeFeedProcessor;
    private readonly ApplicationProfile applicationProfile;
    private readonly ILogger logger;
    public DataBatchProcessor(IOptions<AzureCosmosDbSettings> cosmosDbOptions, ILogger<DataBatchProcessor> logger, ApplicationProfile applicationProfile)
    {
        this.cosmosDbOptions = cosmosDbOptions;
        this.applicationProfile = applicationProfile;
        this.logger = logger;
    }

    public async Task InitializeChangeFeed<T>(string sourceContainerName, string leaseContainerName,
        string serviceName,
        Func<IReadOnlyCollection<T>, CancellationToken, Task> handleChangesAsync, DateTime? startDateTime,
        CancellationToken cancellationToken)
    {
        var configuration = cosmosDbOptions.Value;
        var databaseName = configuration.DatabaseName;
        var cosmosClient = await CosmosClientFactory.GetNewtonCosmosDbClientOptions(true, applicationProfile.AppName, cosmosDbOptions, cancellationToken, sourceContainerName);
        var leaseContainer = await CreateLeaseContainer(cosmosClient, leaseContainerName);
        var feedConfig = cosmosClient.GetContainer(databaseName, sourceContainerName)
            .GetChangeFeedProcessorBuilder<T>(leaseContainerName,
                async (changes, token) => await handleChangesAsync(changes, cancellationToken))
            .WithInstanceName(serviceName)
            .WithErrorNotification(OnErrorMethod)
            .WithLeaseContainer(leaseContainer);
        if (startDateTime.HasValue)
            if (startDateTime.Value == DateTime.MinValue)
                feedConfig = feedConfig.WithStartTime(DateTime.MinValue.ToUniversalTime());
            else
                feedConfig = feedConfig.WithStartTime(startDateTime.Value);
        changeFeedProcessor = feedConfig.Build();
    }

    public async Task Start()
    => await changeFeedProcessor.StartAsync();


    public async Task Stop()
    => await changeFeedProcessor.StopAsync();


    private Task OnErrorMethod(string leaseToken, Exception exception)
    {
        logger.LogCritical(exception.ToString());
        return Task.CompletedTask;
    }

    private async Task<Container> CreateLeaseContainer(CosmosClient cosmosClient, string leaseContainerName)
    {
        var databaseName = cosmosDbOptions.Value.DatabaseName;
        var db = cosmosClient.GetDatabase(databaseName);
        var leaseContainerProperties = new ContainerProperties(leaseContainerName, "/" + "id");
        Container leaseContainer = await db.CreateContainerIfNotExistsAsync(leaseContainerProperties);
        return leaseContainer;
    }
}