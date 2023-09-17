using System.Text.Json.Serialization;

namespace AppVerse;

public record FilesDto
{
    public FilesDto()
    {

    }
    public string Title { get; set; }

    public string FileId { get; set; }
    public string FileName { get; set; }

    public FilesDto(string fileName, string fileType)
    {
        FileName = fileName;
        FileType = fileType;
        File = new MemoryStream();
    }

    public string FileUrl { get; set; }

    [JsonIgnore] public MemoryStream File { get; set; }

    [JsonIgnore] public bool IsFileDeleted { get; set; }

    public string FileType { get; set; }

}