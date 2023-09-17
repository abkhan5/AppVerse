

using PasswordGenerator;
using System.Security.Cryptography;

namespace AppVerse.Service.Authentication;

public class GuidUniqueCodeGeneratorService : IUniqueCodeGeneratorService
{
    public string GetUniqueCode(string inputString = null)
    {
        if (string.IsNullOrEmpty(inputString))
            inputString = Guid.NewGuid().ToString();
        var data = MD5.HashData(Encoding.UTF8.GetBytes(inputString));
        return Convert.ToHexString(data, 0, 4).ToLower();
    }

    public string GetPassword() =>
        new Password().IncludeLowercase().IncludeUppercase().IncludeSpecial().IncludeNumeric().Next();
}