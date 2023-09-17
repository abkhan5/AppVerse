
namespace AppVerse;

public record UserProfileImagesDto : BaseDto
{
    public UserProfileImagesDto()
    {

    }
    public UserProfileImagesDto(string userId, string profileUrl)
    {
        Id = userId;
        UserId = userId;
        CreatedOn = DateTime.UtcNow;
        ProfileUrl = profileUrl;
    }

    public string ProfileUrl { get; set; }
}
