using System.Text.Json.Serialization;

namespace AppVerse;

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
