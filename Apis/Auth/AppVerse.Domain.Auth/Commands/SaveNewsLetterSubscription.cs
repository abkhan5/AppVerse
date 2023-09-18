
namespace AppVerse.Domain.Authentication.Commands;

public record SaveNewsLetterSubscription(string EmailId) : ICommand;
public record BookADemo(string? EmailId, int? PhoneNumber) : ICommand;