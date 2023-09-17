using System.ComponentModel.DataAnnotations;

namespace AppVerse.Api.Authentication.Models;

public record OtpModel
{
    [Required] public string PhoneNumber { get; set; }
    [Required] public string ReceivedOtp { get; set; }
    [Required] public string RequestKey { get; set; }

}