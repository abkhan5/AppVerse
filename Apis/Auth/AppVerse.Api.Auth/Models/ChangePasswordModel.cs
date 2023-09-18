using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record ChangePasswordModel
{
    [DataType(DataType.Password)] public string Password { get; set; }

    [DataType(DataType.Password)] public string UpdatedPassword { get; set; }
}