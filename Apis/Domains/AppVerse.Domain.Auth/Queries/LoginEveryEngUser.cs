
namespace AppVerse.Domain.Authentication.Queries;

public record LoginEveryEngUser(string UserName, string Password) : IQuery<AuthenticationResponseDto>;