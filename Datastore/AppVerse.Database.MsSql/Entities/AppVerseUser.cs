
namespace AppVerse.Database.MsSql.Entities;

public class AppVerseUser : IdentityUser
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsNdaSigned { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string DisplayName { get; set; }
    public virtual Country Country { get; set; }
    public int? CountryId { get; set; }
}

public record RefreshToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Token { get; set; }

    public string JwtId { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool Used { get; set; }

    public bool Invalidated { get; set; }

    public virtual AppVerseUser User{ get; set; }
    public string UserId { get; set; }
}