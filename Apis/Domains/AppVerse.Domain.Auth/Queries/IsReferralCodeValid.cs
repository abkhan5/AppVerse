
namespace AppVerse.Domain.Authentication.Queries;

public record IsReferralCodeValid(string ReferralCode) : IQuery<bool>;