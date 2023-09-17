namespace AppVerse;

public interface IRepositoryStream
{
    Task<string> GetDocument(string id, CancellationToken cancellationToken);
}
