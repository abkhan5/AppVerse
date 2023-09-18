namespace AppVerse.Api.Authentication.Models;

public record EmailModel
{
    public string? UserEmail { get; set; }
}
public record IdRequestModel
{
    public string UserId { get; set; }
}