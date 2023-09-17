using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record CompanyNameModel
{
    [Required] public string CompanyName { get; set; }

    public string EmailId { get; set; }
}