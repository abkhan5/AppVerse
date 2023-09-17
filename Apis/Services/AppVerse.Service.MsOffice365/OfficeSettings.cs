namespace AppVerse.Service.MsOffice365;

public sealed record OfficeSettings
{
    public const string OfficeSettingsOptions = "OfficeSettings";
    public string TenantId { get; set; }
  
    public string Instance { get; set; }
    public string Domain { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string CallbackPath { get; set; }
    public string MeetingOrganizer { get; set; }
}
