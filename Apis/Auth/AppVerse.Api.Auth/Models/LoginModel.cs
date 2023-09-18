using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record LoginModel
{
    [Required] public string UserName { get; set; }


    [DataType(DataType.Password)] public string Password { get; set; }
}
public record CreateProfileModel
{
    public string FirstName { get; set; }

    [PasswordPropertyText]
    public string Password { get; set; }
    [PasswordPropertyText]
    public string ConfirmPassword { get; set; }
    public string LastName { get; set; }

    public string? ReferalCode { get; set; }

    //^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$
    public string EmailId { get; set; }

}