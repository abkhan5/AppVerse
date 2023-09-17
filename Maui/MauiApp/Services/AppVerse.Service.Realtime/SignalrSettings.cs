namespace AppVerse.Service.Realtime;

public class SignalrSettings
{
    public const string SignalrOptions = "SignalrSettings";
    public string HubConnectionString { get; set; }
    public string HubName { get; set; }
}
