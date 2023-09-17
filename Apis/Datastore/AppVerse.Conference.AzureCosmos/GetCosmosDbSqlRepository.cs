using AppVerse.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;

namespace AppVerse.Storage.AzureCosmosDbSql;

public partial class CosmosDbSqlRepository : CosmosBaseRepository, IRepository
{
    public async Task<decimal> GetSum(string queryFilter, CancellationToken cancellationToken)
    {
        var requestOptions = new QueryRequestOptions
        {
            MaxConcurrency = -1,
            PopulateIndexMetrics = true,
        };
        var container = await GetCosmosContainer(false, cancellationToken);
        using FeedIterator<decimal> iterator = container.GetItemQueryIterator<decimal>(new QueryDefinition(queryFilter), requestOptions: requestOptions);
        var countResponse = await iterator.ReadNextAsync(cancellationToken);
        var totalRecords = countResponse.Sum(item => item);
        return totalRecords;
    }
    public async Task<int> GetTotalCount<TEntity>(string queryFilter, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var requestOptions = new QueryRequestOptions
        {
            MaxConcurrency = -1,
            PopulateIndexMetrics = true
        };
        var container = await GetCosmosContainer(false, cancellationToken);
        using var iterator = container.GetItemQueryIterator<int>(GetCountQueryDefinitation<TEntity>(queryFilter),
            requestOptions: requestOptions);
        var countResponse = await iterator.ReadNextAsync(cancellationToken);
        var totalRecords = countResponse.Sum(item => item);
        return totalRecords;
    }
    public async Task<int> GetTotalCount<TEntity>(CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var query = container.GetItemLinqQueryable<TEntity>(true);
        var entityName = Activator.CreateInstance<TEntity>().Discriminator;
        return query.Where(item => item.Discriminator == entityName).Count();
    }
    public async Task<int> GetTotalCount<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var query = container.GetItemLinqQueryable<TEntity>(true);
        var entityName = typeof(TEntity).Name;
        return query.Where(predicate).Where(item => item.Discriminator == entityName).Count();
    }


    public async Task<TEntity> GetItem<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var query = container.GetItemLinqQueryable<TEntity>(true);
        var entityName = typeof(TEntity).Name;
        var filter = query.Where(predicate).Where(item => item.Discriminator == entityName);
        TEntity response = null;
        foreach (var item in filter)
        {
            response = item;
            break;
        }

        return response;
    }
    public async Task<TEntity> Get<TEntity>(string id, string partitionKey, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        try
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(partitionKey))
                return null;
            var container = await GetCosmosContainer(false, cancellationToken);
            var itemResponse =
                await container.ReadItemAsync<TEntity>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
            return itemResponse.Resource;
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<TEntity> Get<TEntity>(string id, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        try
        {
            if (string.IsNullOrEmpty(id))
                return null;
            var container = await GetCosmosContainer(false, cancellationToken);
            var itemResponse =
                await container.ReadItemAsync<TEntity>(id, new PartitionKey(id), cancellationToken: cancellationToken);
            return itemResponse.Resource;
        }
        catch (Microsoft.Azure.Cosmos.CosmosException e)
        {
            return null;
        }
    }

    public async IAsyncEnumerable<TEntity> GetAll<TEntity>(CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var sqlQuery = GetDescriptorQueryDefinitation<TEntity>();
        var container = await GetCosmosContainer(false, cancellationToken);
        using var feedIterator = container.GetItemQueryIterator<TEntity>(sqlQuery,
            requestOptions: new QueryRequestOptions
            {
                MaxConcurrency = -1
            });
        while (feedIterator.HasMoreResults)
        {
            var feedResponse = await feedIterator.ReadNextAsync(cancellationToken);

            foreach (var responseItem in feedResponse)
                yield return responseItem;
        }
    }

    public async IAsyncEnumerable<TEntity> GetAll<TEntity>(string queryString, CancellationToken cancellationToken)
        where TEntity : BaseDto
    {
        var sqlQuery = GetDescriptorQueryDefinitation<TEntity>(queryString);
        var container = await GetCosmosContainer(false, cancellationToken);
        using var feedIterator = container.GetItemQueryIterator<TEntity>(sqlQuery,
            requestOptions: new QueryRequestOptions
            {
                MaxConcurrency = -1
            });

        while (feedIterator.HasMoreResults)
        {
            var feedResponse = await feedIterator.ReadNextAsync(cancellationToken);
            foreach (var responseItem in feedResponse)
                yield return responseItem;
        }
    }

    public async IAsyncEnumerable<TEntity> Get<TEntity, TKey>(Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TKey>> keySelector, bool isDescending, CancellationToken cancellationToken)
        where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var query = container.GetItemLinqQueryable<TEntity>();

        var instance = Activator.CreateInstance<TEntity>();
        var entityDiscriminator = instance.Discriminator;
        var orderdQuery = predicate == null ? query : query.Where(predicate);
        orderdQuery = isDescending ? orderdQuery.OrderByDescending(keySelector) : orderdQuery.OrderBy(keySelector);
        var feedIterator = orderdQuery.Where(item => item.Discriminator == entityDiscriminator).ToFeedIterator();
        while (feedIterator.HasMoreResults)
        {
            var feedResponse = await feedIterator.ReadNextAsync(cancellationToken);
            foreach (var responseItem in feedResponse)
                yield return responseItem;
        }
    }

    public async IAsyncEnumerable<TEntity> GetOrderedBy<TEntity, TKey>(Expression<Func<TEntity, TKey>> keySelector,
        bool isDescending, CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var query = container.GetItemLinqQueryable<TEntity>();
        var entityName = typeof(TEntity).Name;
        var orderdQuery = query.Where(item => item.Discriminator == entityName);
        orderdQuery = isDescending ? orderdQuery.OrderByDescending(keySelector) : orderdQuery.OrderBy(keySelector);
        var feedIterator = orderdQuery.ToFeedIterator();
        while (feedIterator.HasMoreResults)
        {
            var feedResponse = await feedIterator.ReadNextAsync(cancellationToken);
            foreach (var responseItem in feedResponse)
                yield return responseItem;
        }
    }

    public async IAsyncEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken) where TEntity : BaseDto
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        var query = container.GetItemLinqQueryable<TEntity>();
        var instance = Activator.CreateInstance<TEntity>();
        var entityDiscriminator = instance.Discriminator;
        var feedIterator = query.Where(predicate).Where(item => item.Discriminator == entityDiscriminator).ToFeedIterator();
        while (feedIterator.HasMoreResults)
            foreach (var responseItem in await feedIterator.ReadNextAsync(cancellationToken))
                yield return responseItem;
    }

    public async IAsyncEnumerable<TEntity> GetEntities<TEntity>(string queryFilter, CancellationToken cancellationToken)
    {
        var container = await GetCosmosContainer(false, cancellationToken);
        using var feedIterator = container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryFilter));
        var feedResponse = await feedIterator.ReadNextAsync(cancellationToken);
        foreach (var responseItem in feedResponse)
            yield return responseItem;
    }


}
