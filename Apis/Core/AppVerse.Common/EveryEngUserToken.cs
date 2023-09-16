namespace AppVerse;

public record EveryEngUserToken
{
    public EveryEngUserToken(string createUserToken, bool isPhoneNumber)
    {
        CreateUserToken = createUserToken;
        IsPhoneNumber = isPhoneNumber;
    }
    public bool IsPhoneNumber { get; set; }
    public string CreateUserToken { get; set; }
}
