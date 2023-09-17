namespace AppVerse.Services;

public interface IAppRepository
{
    Task<bool> SaveNewLetterSubscription(string emailId, CancellationToken cancellationToken);
    Task BookADemo(string emailId, int? phoneNumber, CancellationToken cancellationToken);
}