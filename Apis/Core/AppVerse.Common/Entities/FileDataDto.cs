namespace AppVerse;

public record FileDataDto
{
    public string FileId { get; set; }
    public string FileName { get; set; }
    public byte[] FileBytes { get; set; }
    public string FileType { get; set; }
    public string Title { get; set; }
}
