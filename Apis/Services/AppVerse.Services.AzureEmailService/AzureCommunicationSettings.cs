namespace AppVerse.Service.AzureCommunicationSevice;

public record AzureCommunicationSettings
{
    public const string AzureCommunicationSettingsName = "AzureCommunicationSettings";
    public string ConnectionString { get; set; }
}