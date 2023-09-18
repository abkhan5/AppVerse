namespace AppVerse.Domain.Authentication.Commands;

public record ForgotPasswordEmail(string EmailId) : ICommand;