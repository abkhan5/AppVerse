namespace AppVerse.Domain.Authentication.Queries;

public record ValidateOtp(string PhoneNumber, string Otp, string RequestKey) : IQuery<AuthenticationResponseModel>;