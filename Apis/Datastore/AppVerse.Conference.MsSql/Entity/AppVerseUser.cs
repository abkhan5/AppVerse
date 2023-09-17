
namespace AppVerse.Conference.MsSql.Entity;

public enum LoginSourceEnum
{
    AppVerse,
    Google
}

public  class AppVerseUser : IdentityUser
{
    public AppVerseUser()
    {
    }

    public AppVerseUser(string userName) : base(userName)
    {
    }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ReferralCode { get; set; }
    public bool IsNdaSigned { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string DisplayName { get; set; }
}
