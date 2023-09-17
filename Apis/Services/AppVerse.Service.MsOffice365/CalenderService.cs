using AppVerse.Services;
using Microsoft.Extensions.Logging;

namespace AppVerse.Service.MsOffice365;
public class CalenderService : ICalenderService
{
    private readonly ILogger<CalenderService> logger;
    private readonly OfficeClientService officeService;
    public CalenderService(OfficeClientService officeService, ILogger<CalenderService> logger)
    {
        this.officeService = officeService;
        this.logger = logger;
    }
    public async Task UpdateCalender(CalenderEntry entryItem, CancellationToken cancellationToken)
    {
        var graphClient = officeService.GetGraphClient();
       // var item = await graphClient.Users["d5sdf9be93d-e48b-4bf7-8d9d-e28e11ba4704"].GetAsync(cancellationToken: cancellationToken);
       // await graphClient.Me.CalendarView.GetAsync(cancellationToken: cancellationToken);

        //var meItem = graphClient.Me;
        //logger.LogInformation($"{meItem.} ... {item.MailNickname}... {item.GivenName}  ... {hasCalender} ...{item}");
        //foreach (var item in users.)
        {
         //   var calnder = item.CalendarView;
           // var hasCalender = item.CalendarView != null ? "has calender" : " no calender";
           // logger.LogInformation($"{item.Id} ... {item.MailNickname}... {item.GivenName}  ... {hasCalender} ...{item.Mail}");
        }
        // item.CalendarView


    }
}

