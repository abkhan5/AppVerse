using AppVerse.Service.Realtime;

namespace Microsoft.Extensions.DependencyInjection;

public static class SerivicesExtension
{
    public static void AddEveryEngServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SignalrSettings>(configuration.GetSection(SignalrSettings.SignalrOptions));
        services.TryAddSingleton<RealTimeService>();
        services.TryAddSingleton<IRealTimeService>(sp =>
        {
            var realTimeService = sp.GetRequiredService<RealTimeService>();
            realTimeService.Initialize();
            return realTimeService;

        });
        services.AddTransient<IRestService, RestService>();
    }
}