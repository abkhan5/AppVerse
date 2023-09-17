using AppVerse.Services;
using AppVerse.Services.RealtimeService;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class AzureServiceExtensions
{
    public static void AddRealTimeService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IRealTimeService, SignalRService>();        
    }
}