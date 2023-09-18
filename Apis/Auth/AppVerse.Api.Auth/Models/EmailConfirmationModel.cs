using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record EmailConfirmationModel
{
    [Required(ErrorMessage = "The UserEmail cannot be blank.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email")]
    public string UserEmail { get; set; }

    public string? Token { get; set; }
}