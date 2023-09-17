
namespace AppVerse.Domain.Authentication.Commands;

public record EmailConfirmation(string UserEmail, string Token) : ICommand;