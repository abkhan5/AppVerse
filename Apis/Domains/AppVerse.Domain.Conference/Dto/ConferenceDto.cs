namespace AppVerse.Domain.Conference.Dto;

internal record ConferenceDto : BaseDto
{
    public List<UserProfileDto> Hosts { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime End { get; set; }
    public SmartContentDto Content { get; set; }
    public List<Agenda> ConferanceAgenda{ get; set; }
}

public record Agenda:BaseDto
{
    public List<UserProfileDto> Hosts { get; set; }
    public List<UserProfileDto> Attendes{ get; set; }
    public DateTime StartDate { get; set; }
    public DateTime End { get; set; }
    public SmartContentDto Content { get; set; }
}