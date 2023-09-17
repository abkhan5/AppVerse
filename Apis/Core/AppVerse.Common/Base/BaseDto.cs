using System.Text.Json.Serialization;

namespace AppVerse.Base;

public abstract record BaseDto
{
    private string discriminator;

    [JsonPropertyName("id")] public string Id { get; set; }

    public string UserId { get; set; }

    public string Discriminator
    {
        get => GetDiscriminator();
        set => discriminator = value;
    }

    public DateTime CreatedOn { get; set; }

    protected virtual string GetDiscriminator()
    {
        if (string.IsNullOrEmpty(discriminator))
            discriminator = GetType().Name;
        return discriminator;
    }
}

public record UserEventDto : BaseDto
{
    public string EventName { get; set; }
    public string Message { get; set; }
    public bool IsError { get; set; }
    public string Location { get; set; }
}

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