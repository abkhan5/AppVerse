using AppVerse.Conference.AzureBlobStore;
using AppVerse.Services;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class AzureServiceExtensions
{
    public static void AddAzureBlobStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAppRepository, AppRepository>();
        services.AddTransient<IUserEventStore, UserEventStore>();
        services.AddTransient<IFileService, FileService>();

    }

}