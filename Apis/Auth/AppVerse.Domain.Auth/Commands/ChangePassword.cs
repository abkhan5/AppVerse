using System.ComponentModel.DataAnnotations;

namespace AppVerse.Domain.Authentication.Commands;

public record ChangePassword([DataType(DataType.Password)] string Password, [DataType(DataType.Password)] string UpdatedPassword) : ICommand;