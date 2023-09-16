namespace AppVerse.Service.Authentication;

internal record AppVerseUserDto
{
    public const string CacheKey = "eeauthuser";
    public AppVerseUserDto()
    {

    }
    public AppVerseUserDto(AppVerseUser everyEngUser)
    {
        UserName = everyEngUser.UserName;
        IsActive = everyEngUser.LockoutEnabled;
    }
    public string UserName { get; set; }
    public bool IsActive { get; set; }

    public static string CreateCacheKey(string userName) => $"{CacheKey}~{userName}";
};
