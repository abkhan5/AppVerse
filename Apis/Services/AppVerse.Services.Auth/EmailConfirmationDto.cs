
namespace AppVerse.Service.Authentication;

public record EmailConfirmationDto
{
    public string UserEmail { get; set; }

    public string Token { get; set; }

    public string Address { get; set; }
    public string Path { get; set; }
}
