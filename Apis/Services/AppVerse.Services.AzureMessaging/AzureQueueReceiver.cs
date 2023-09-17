//using Azure.Messaging.ServiceBus;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace AppVerse.Services.AzureMessaging;

//internal class AzureQueueReceiver : IMessageReceiver
//{
//    private readonly AzureEntityManager azureEntityManager;
//    private readonly ILogger<AzureQueueReceiver> logger;
//    private readonly ServiceBusClient queueClient;
//    private readonly IOptions<AzureServiceBusSettings> serviceBusSettings;
//    private ServiceBusReceiver processor;

//    public AzureQueueReceiver(IOptions<AzureServiceBusSettings> serviceBusSettings,
//        ILogger<AzureQueueReceiver> logger,
//        AzureEntityManager azureEntityManager,
//        ServiceBusClient queueClient)
//    {
//        this.serviceBusSettings = serviceBusSettings;
//        this.logger = logger;
//        this.azureEntityManager = azureEntityManager;
//        this.queueClient = queueClient;
//    }

//    public string QueueName => throw new NotImplementedException();

//    async Task IMessageReceiver.OnMessageAsync(string queueName, Func<CancellationToken, Task> callBack,
//        CancellationToken token)
//    {
//        await azureEntityManager.CreateQueue(queueName, token);
//        queueName = azureEntityManager.GetEnvQueueName(queueName);
//        logger.LogInformation($"Receiver set for {queueName}");
//        processor = queueClient.CreateReceiver(queueName);
//        var receivedMessage = await processor.ReceiveMessageAsync(cancellationToken: token);
//        await callBack(token);
//        await processor.CompleteMessageAsync(receivedMessage, token);
//    }

//    async Task IMessageReceiver.OnMessageAsync<T>(string queueName, Func<T, Task> callBack, CancellationToken token)
//    {
//        await OnMessageAsync(queueName, callBack, token);
//    }


//    async Task IMessageReceiver.OnMessageAsync<T>(string queueName, Func<T, CancellationToken, Task> callBack,
//        CancellationToken token)
//    {
//        await OnMessageAsync(queueName, callBack, token);
//    }


//    public async Task Stop(CancellationToken token)
//    {
//        await processor.CloseAsync(token);
//    }


//    //private async Task OnMessageAsync<T>(string queueName, Func<T, Task> callBack, CancellationToken token)
//    //{
//    //queueName = azureEntityManager.GetEnvQueueName(queueName);
//    //    await using var queueClient = new ServiceBusClient(serviceBusSettings.Value.ConnectionString);
//    //    processor = queueClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());
//    //    processor.ProcessMessageAsync += async responseItem => 
//    //    {
//    //        var payload = JsonSerializer.Deserialize<T>(responseItem.Message.Body);
//    //        await callBack(payload);
//    //    };
//    //    processor.ProcessErrorAsync += Processor_ProcessErrorAsync;
//    //    await processor.StartProcessingAsync(token);
//    //}


//    public async ValueTask DisposeAsync()
//    {
//        await processor.DisposeAsync();
//        await queueClient.DisposeAsync();
//    }

//    private async Task OnMessageAsync<T>(string queueName, Func<T, Task> callBack, CancellationToken token)
//    {
//        await azureEntityManager.CreateQueue(queueName, token);
//        queueName = azureEntityManager.GetEnvQueueName(queueName);
//        logger.LogInformation($"Receiver set for {queueName}");
//        processor = queueClient.CreateReceiver(queueName);
//        var receivedMessage = await processor.ReceiveMessageAsync(cancellationToken: token);
//        var payload = receivedMessage.Body.ToObjectFromJson<T>();
//        await callBack(payload);
//        await processor.CompleteMessageAsync(receivedMessage, token);
//    }

//    private async Task OnMessageAsync<T>(string queueName, Func<T, CancellationToken, Task> callBack,
//        CancellationToken token)
//    {
//        await azureEntityManager.CreateQueue(queueName, token);
//        queueName = azureEntityManager.GetEnvQueueName(queueName);
//        logger.LogInformation($"Receiver set for {queueName}");
//        await using var queueClient = new ServiceBusClient(serviceBusSettings.Value.ConnectionString);
//        processor = queueClient.CreateReceiver(queueName);
//        var receivedMessage = await processor.ReceiveMessageAsync(cancellationToken: token);
//        var payload = receivedMessage.Body.ToObjectFromJson<T>();
//        await callBack(payload, token);
//        await processor.CompleteMessageAsync(receivedMessage, token);
//    }
//}