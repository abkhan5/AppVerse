using AppVerse;
using AppVerse.Services.AzureOtp;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;
public static class AzureServiceExtensions
{
    public static void AddAzureOtpService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOtpService, AzureOtpServices>();
        
    }

}