



namespace AppVerse.Domain.Authentication.Commands;
public record CreateProfile(string FirstName, string LastName, string Password, string ConfirmPassword, string ReferalCode, string EmailId, LoginSourceEnum Source) : ICommand;