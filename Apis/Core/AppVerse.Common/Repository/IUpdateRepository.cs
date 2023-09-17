namespace AppVerse;

public interface IUpdateRepository
{
    Task Update<TEntity>(TEntity entity, string partitionKey, CancellationToken cancellationToken) where TEntity : BaseDto;
    Task Update<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseDto;

    Task Upsert<TEntity>(TEntity entity, string partitionKey = null, CancellationToken cancellationToken = default)
        where TEntity : BaseDto;

    Task Upsert<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseDto;
}
