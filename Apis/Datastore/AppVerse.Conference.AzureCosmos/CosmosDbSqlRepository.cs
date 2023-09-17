using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace AppVerse.Storage.AzureCosmosDbSql;


public partial class CosmosDbSqlRepository : CosmosBaseRepository, IRepository
{
    public CosmosDbSqlRepository(IOptions<AzureCosmosDbSettings> cosmosDbOptions, CosmosClientFactory cosmosClient) :
        base(cosmosDbOptions, cosmosClient)
    {
    }

    public async Task Delete<TEntity>(string id, string partitionKey, CancellationToken cancellationToken)
        where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        await container.DeleteItemAsync<TEntity>(id, new PartitionKey(partitionKey),
            cancellationToken: cancellationToken);
    }

    public async Task DeleteAll<TEntity>(CancellationToken cancellationToken) where TEntity : BaseDto
    {
        await BulkOperateAsync(GetAll<TEntity>(cancellationToken), CosmosOperations.Delete, cancellationToken);
    }

    public async Task Delete<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var communityExists = await Get<TEntity>(entity.Id, cancellationToken);
        if (communityExists != null)
            await container.DeleteItemAsync<TEntity>(entity.Id, new PartitionKey(entity.Id),
                cancellationToken: cancellationToken);
    }


    public async Task Upsert<TEntity>(TEntity entity, string partitionKey = null,
        CancellationToken cancellationToken = default) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        if (string.IsNullOrEmpty(partitionKey))
            await container.UpsertItemAsync(entity, cancellationToken: cancellationToken);
        else
            await container.UpsertItemAsync(entity, new PartitionKey(partitionKey),
                cancellationToken: cancellationToken);
    }

    public async Task Upsert<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);

        await container.UpsertItemAsync(entity, new PartitionKey(entity.Id), cancellationToken: cancellationToken);
    }

    public async Task Update<TEntity>(TEntity entity, string partitionKey, CancellationToken cancellationToken)
        where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);

        await container.ReplaceItemAsync(entity, entity.Id, new PartitionKey(partitionKey),
            cancellationToken: cancellationToken);
    }

    public async Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);

        await container.ReplaceItemAsync(entity, entity.Id, new PartitionKey(entity.Id),
            cancellationToken: cancellationToken);
    }


    public async Task Create<TEntity>(TEntity entity, string partitionKey, CancellationToken cancellationToken)
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        await container.CreateItemAsync(entity, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
    }


    public async Task Create<TEntity>(TEntity entity, CancellationToken cancellationToken)
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        await container.CreateItemAsync(entity, cancellationToken: cancellationToken);
    }
    public async Task Create<TEntity>(string id, string partitionKey, TEntity entity,
        CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        await container.ReplaceItemAsync(partitionKey: new PartitionKey(partitionKey), id: id,item: entity, cancellationToken: cancellationToken);
    }  
  
    #region Bulk Save Entites

    public async Task BulkOperateAsync<TEntity>(IAsyncEnumerable<TEntity> entities, CosmosOperations cosmosOperations,
        CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        List<Task> concurrentTasks = new();
        await foreach (var entityItem in entities)
        {
            AddOperationTask(concurrentTasks, entityItem, cosmosOperations, container, cancellationToken);
            if (concurrentTasks.Count > 50)
            {
                await Task.WhenAll(concurrentTasks);
                await Task.Delay(1000, cancellationToken);
                concurrentTasks.Clear();
            }
        }

        await Task.WhenAll(concurrentTasks);
    }

    private static void AddOperationTask<TEntity>(List<Task> concurrentTasks, TEntity entityItem,
        CosmosOperations cosmosOperations, Container container, CancellationToken cancellation) where TEntity : BaseDto
    {
        Task operationTask = null;
        switch (cosmosOperations)
        {
            case CosmosOperations.Delete:
                operationTask = container.DeleteItemAsync<TEntity>(entityItem.Id, new PartitionKey(entityItem.Id),
                    cancellationToken: cancellation);
                break;
            case CosmosOperations.Update:
                operationTask = container.ReplaceItemAsync(entityItem, entityItem.Id, new PartitionKey(entityItem.Id),
                    cancellationToken: cancellation);
                break;
            case CosmosOperations.Upsert:
                operationTask = container.UpsertItemAsync(entityItem, new PartitionKey(entityItem.Id),
                    cancellationToken: cancellation);
                break;
            case CosmosOperations.Add:
                operationTask = container.CreateItemAsync(entityItem, new PartitionKey(entityItem.Id),
                    cancellationToken: cancellation);
                break;
        }

        if (operationTask == null)
            return;

        concurrentTasks.Add(operationTask);
    }

    #endregion
}