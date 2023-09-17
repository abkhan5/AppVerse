
namespace AppVerse.Domain.Auth.Dto;
public record UserCreatedDto(string EmailId, string ProfileId, string ReferralCode, LoginSourceEnum LoginSource)
{
    public const string appverseUserCreated = "OnNewMember";
}