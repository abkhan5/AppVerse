
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppVerse.Conference.MsSql.Entity;

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

    public virtual AppVerseUser Profile { get; set; }
    public string ProfileId { get; set; }
}
public record ConferenceEvent : BaseEntity
{
    public string Title{ get; set; }
    public AppVerseUser User { get; set; }
    public string UserId { get; set; }

    public string Location { get; set; }
    
    public Collection<ConferenceAgenda> Agenda { get; set; }

    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}