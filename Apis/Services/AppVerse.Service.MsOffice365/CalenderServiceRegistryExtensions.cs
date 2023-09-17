using AppVerse.Service.MsOffice365;
using AppVerse.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Microsoft.Identity.Client;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection;

public static class Office365ServiceRegistryExtensions
{
    public static IServiceCollection AddCalenderService(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<OfficeSettings>(configuration.GetSection(OfficeSettings.OfficeSettingsOptions));

        services.TryAddTransient<ICalenderService, CalenderService>();
        services.TryAddSingleton<OfficeClientService>();
        services.TryAddSingleton<IMeetingService,TeamMeetingsServices>();
        return services;
    }
}