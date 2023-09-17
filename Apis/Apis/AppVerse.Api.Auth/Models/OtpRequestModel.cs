using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record LoginRequestModel
{
    [Required] public string UserLoginInput { get; set; }
}
