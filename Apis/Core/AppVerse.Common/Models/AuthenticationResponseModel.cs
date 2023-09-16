using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Models;

public record AuthenticationResponseModel
{
    public string Token { get; set; }

    public string RefreshToken { get; set; }

    public bool IsProfileActive { get; set; }
    public string ProfileId { get; set; }
}