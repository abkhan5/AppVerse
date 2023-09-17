namespace AppVerse;
public interface IRepository : IGetRepository, IDeleteRepository, IPatchRepository, IUpdateRepository, ICreateRepository
{
    public const int MaxItemCount = 50;
    void SetContainerName(string containerName);

    Task BulkOperateAsync<TEntity>(IAsyncEnumerable<TEntity> entities, CosmosOperations cosmosOperations,
        CancellationToken cancellationToken) where TEntity : BaseDto;

}

public enum CosmosOperations
{
    Delete,
    Update,
    Upsert,
    Add
}