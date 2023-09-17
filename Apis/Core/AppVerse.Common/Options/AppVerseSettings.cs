namespace AppVerse.Options;

public sealed record AppServiceSettings
{
    public const string appverseServiceOptions = "AppServiceSettings";
    public string ServiceHost { get; set; }
    public string ResetPasswordRoute { get; set; }
    public string VerifyAccountRoute { get; set; }
    public string NotificationServiceHost { get; set; }
    public string MessengerHubServiceHost { get; set; }
    public string AdminUserCredentials { get; set; }
    public string AuthServiceHost { get; set; }
    public string ConfirmationEmailPath { get; set; }
}