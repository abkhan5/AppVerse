

namespace AppVerse.Domain.Authentication.Queries;
public record OnRefreshToken(string RefreshToken) : IQuery<AuthenticationResponseModel>;