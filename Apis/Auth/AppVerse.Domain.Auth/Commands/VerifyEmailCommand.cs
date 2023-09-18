namespace AppVerse.Domain.Authentication.Commands;

public record VerifyEmailCommand(string UserEmail) : ICommand;