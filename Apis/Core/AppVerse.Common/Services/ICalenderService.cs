using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVerse.Services;

public interface ICalenderService
{
    Task UpdateCalender(CalenderEntry entryItem, CancellationToken cancellationToken);
}

public interface IMeetingService
{
    Task<MeetingDto> CreateTeamsMeeting(MeetingDto meeting, CancellationToken cancellationToken);
    Task Cancel(MeetingDto meeting, CancellationToken cancellationToken);
}


public record MeetingDto : BaseDto
{
    public MeetingDto()
    {
        Links = new List<string>();
    }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Title { get; set; }
    public string RecordId { get; set; }
    public List<string> Links { get; set; }
}


public record CalenderEntry
{
    public CalenderEntryActionEnum Action { get; set; }
    public DateTime EntryDate { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
}
public enum CalenderEntryActionEnum
{
    Upsert,
    Delete
}