
namespace AppVerse.Domain.Authentication.Commands;

public record ResetAndChangePassword(string Email, string Token, string Password, string ConfirmPassword) : ICommand;