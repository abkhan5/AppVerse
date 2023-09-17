using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record ForgotPasswordModel
{
    [Required][EmailAddress] public string Email { get; set; }
}