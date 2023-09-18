
namespace AppVerse.Domain.Authentication.Queries;

public record LoginappverseUser(string UserName, string Password) : IQuery<AuthenticationResponseModel>;