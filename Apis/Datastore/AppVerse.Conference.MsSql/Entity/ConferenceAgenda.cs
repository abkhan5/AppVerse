namespace AppVerse.Conference.MsSql.Entity;

public record ConferenceAgenda:BaseEntity
{
    public string Title { get; set; }
    public Collection<DateTime> EventDates { get; set; }

    public Collection<AppVerseUser> Hosts { get; set; }
    public Collection<AppVerseUser> Attendees { get; set; }

}