namespace AppVerse.Api.Authentication.Models;

public record UserEnquiryModel
{
    public string EmailId { get; set; }

    public string Subject { get; set; }
    public string Message { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string? PhoneNumber { get; set; }
}