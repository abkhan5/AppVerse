namespace AppVerse;

public record ErrorMessageResponse
{
    public List<ErrorModel> Errors { get; set; }
}
