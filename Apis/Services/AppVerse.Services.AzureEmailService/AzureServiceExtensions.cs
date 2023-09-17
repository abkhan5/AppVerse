
namespace Microsoft.Extensions.DependencyInjection;

public static class AzureServiceExtensions
{
    public static void AddAzureEmailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService, AzureEmailService>();
        services.Configure<AzureCommunicationSettings>(configuration.GetSection(AzureCommunicationSettings.AzureCommunicationSettingsName));
        services.TryAddTransient<AzureEmailClientService>();
        services.AddSingleton(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<AzureCommunicationSettings>>();
            return new EmailClient(settings.Value.ConnectionString);
        });
    }
}
