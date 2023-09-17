
namespace AppVerse.Domain.Authentication.Queries;

public record IsIdValid(string UserId) : IQuery<EveryEngUserToken>;