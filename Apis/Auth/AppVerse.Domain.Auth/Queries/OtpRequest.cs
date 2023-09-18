namespace AppVerse.Domain.Authentication.Queries;

public record LoginRequest(string UserLoginInput) : IQuery<string>;