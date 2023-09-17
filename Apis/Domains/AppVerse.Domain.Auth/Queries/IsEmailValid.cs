
namespace AppVerse.Domain.Authentication.Queries;

public record IsEmailValid(string UserEmail) : IQuery<UserToken>;