
namespace AppVerse;
public record UserProfileDto:BaseDto
{
    public string DisplayName { get; set; }
    public string ProfileUrl { get; set; }
}

public record SmartContentDto
{
    public string PlainText { get; set; }
    public string Content { get; set; }
}