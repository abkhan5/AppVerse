
using System.Text;

namespace AppVerse;

public record ApplicationProfile : BaseDto
{
    public string AppName { get; set; }
    public string BuildId { get; set; }
    public string MachineName { get; set; }
    public string ApplicationName { get; set; }
}
public record BlobStorageItemDto
{
    public string FolderName { get; set; }
    private string containerName;
    private string fileName;

    public string ContainerName
    {
        get => containerName;
        set => containerName = value?.ToLower();
    }

    public string FileName
    {
        get => fileName;
        set => fileName = value?.ToLower();
    }


    public string ContentType { get; set; }
    public IDictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();

    public IDictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
    public Stream DataStream { get; set; }
    public bool IsContainerPrivate { get; set; }
    public bool ToEncryptContent { get; set; }
    public static string ToAzureKeyString(string str)
    {
        var sb = new StringBuilder();
        foreach (var c in str
                     .Where(c => c != '/'
                                 //    && c != '\\'
                                 && c != '#'
                                 && c != '/'
                                 && c != '?'
                                 && c != '@'
                                 && !char.IsControl(c)))
            sb.Append(c);
        return sb.ToString();
    }
}