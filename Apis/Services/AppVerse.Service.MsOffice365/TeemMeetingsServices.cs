using AppVerse.Services;
using Microsoft.Graph;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppVerse.Service.MsOffice365;

public class TeamMeetingsServices : IMeetingService
{
    private readonly OfficeClientService officeService;
    public TeamMeetingsServices(OfficeClientService officeService)
    {
        this.officeService = officeService;
    }

    public Task Cancel(MeetingDto meeting, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<MeetingDto> CreateTeamsMeeting(MeetingDto meeting, CancellationToken cancellationToken)
    {
        var graphServiceClient = officeService.GetGraphClient();
        var userId = await officeService.GetUserIdAsync();

        var onlineMeeting = new OnlineMeeting
        {
            StartDateTime = meeting.Start,
            EndDateTime = meeting.End,
            Subject = meeting.Title,
            LobbyBypassSettings = new LobbyBypassSettings
            {
                Scope = LobbyBypassScope.Everyone
            }
        };
        var user = graphServiceClient.Users[userId];
        var meetingResponse = await user
          .OnlineMeetings
          .Request()
          .AddAsync(onlineMeeting, cancellationToken);

        meeting.Links.Add(meetingResponse.JoinUrl);
        meeting.Links.Add(meetingResponse.JoinWebUrl);


        var calender = await user.Calendar.Request().GetAsync(cancellationToken);


        calender.CalendarView.Add(new Event
        {


        });

        return meeting;
    }

 
}
