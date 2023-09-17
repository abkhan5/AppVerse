using AppVerse;

namespace AppVerse;

public interface IDeleteRepository
{
    Task Delete<TEntity>(string id, string partitionKey, CancellationToken cancellationToken) where TEntity : BaseDto;
    Task DeleteAll<TEntity>(CancellationToken cancellationToken) where TEntity : BaseDto;
    Task Delete<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : BaseDto;
}
