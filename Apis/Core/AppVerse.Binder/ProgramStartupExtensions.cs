using AppVerse;
using AppVerse.Infrastructure.Events;
using Azure.Core;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ProgramStartupExtensions
{
    internal static string ApplicationName;
    public static void CreateSerilogLogger(string appName)
    {
        ApplicationName = appName;
    }

    public static async Task RunConsoleHost<T>(this IHostBuilder builder) where T : IAppVerseStartup, new()
    {
        builder.ConfigureAppConfiguration((hostingContext, config) => GetConfiguration(hostingContext.HostingEnvironment, config));
        builder.ConfigureServices((context, services) =>
        {
            var config = context.Configuration;
            var appverseStartup = new T();
            appverseStartup.ConfigureServices(services, config);
            services.AddApplicationProfile();
        });

        var app = builder.Build();
        await app.RunHost();

    }
    private static async Task RunHost<T>(this T app) where T : IHost
    {
        await app.RunAsync();
    }

    public static async Task RunWebHost<T>(this WebApplicationBuilder builder) where T : IAppVerseStartup, new()
    {
        Log.Logger.Information($"Current Env is {builder.Environment.EnvironmentName}");
        builder.Host.ConfigureappverseHost();
        var appverseStartup = new T();
        appverseStartup.ConfigureServices(builder.Services, builder.Configuration);
        builder.Services.AddApplicationProfile();
        builder.AddKestrelExtensions();
        var app = builder.Build();
        appverseStartup.ConfigureApplication(app);
        app.MapControllers();
        await app.RunHost();
    }

    public static void AddTypes(this IServiceCollection services, IAppVerseStartup startup)
    {
        var appTypes = startup.GetAppTypes().ToArray();
        services.AddAutoMapper(appTypes);
        services.ConfigureMediator(appTypes);
        services.AddValidatorsFromAssemblies(appTypes.Select(item=> item.Assembly));
    }

    public static IHostBuilder ConfigureappverseHost(this IHostBuilder host) =>
        host
        .UseSerilog()
        .ConfigureAppConfiguration((hostingContext, config) => GetConfiguration(hostingContext.HostingEnvironment, config));


    private static void GetConfiguration(IHostEnvironment hostingContext, IConfigurationBuilder appConfig)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appverseappsettings.json", false, true)
            .AddEnvironmentVariables();
        var config = builder.Build();
        appConfig.AddConfiguration(config);

        var useVault = config.GetValue("UseVault", false);
        if (useVault)
            appConfig.AddKeyVault(config);
    }
    public static void AddKeyVault(this IConfigurationBuilder appConfig, IConfigurationRoot config)
    {
        var keyVaultName = config["AzureKeyVaultName"];
        var managedId = config["ManagedIdentityId"];
        var tenantId = config["TenantId"];
        Log.Logger.Error($"tenantId : {tenantId}... ManagedId {managedId}  ... ");

        var useMachine = config.GetValue("useMachineName", false);

        Log.Logger.Error($"tenantId : {tenantId}... ManagedId {managedId}  ... useMachine :{useMachine} ");


        TokenCredential userCred = useMachine ? new DefaultAzureCredential(new DefaultAzureCredentialOptions()
        {
            VisualStudioTenantId = tenantId,
            SharedTokenCacheTenantId = tenantId,
            ManagedIdentityClientId = managedId
        }) : new ManagedIdentityCredential(clientId: managedId); 

        var options = new AzureKeyVaultConfigurationOptions
        {
            ReloadInterval = TimeSpan.FromMinutes(15),
            Manager = new KeyVaultSecretManager()
        };
        var url = $"https://{keyVaultName}.vault.azure.net";
        var client = new SecretClient(new Uri(url), userCred);
        appConfig.AddAzureKeyVault(client, options);
    }

    private static void AddKestrelExtensions(this WebApplicationBuilder builder)
    {
        builder.WebHost.CaptureStartupErrors(true);

        builder.WebHost.ConfigureKestrel(options => ConfigureProtocols(options));
    }
    private static void ConfigureProtocols(KestrelServerOptions options)
    {
        options.AddServerHeader = false;
    }

    private static void AddApplicationProfile(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            return new ApplicationProfile
            {
                Id = ApplicationName,
                AppName = ApplicationName,
                UserId = Environment.MachineName,
                MachineName = Environment.MachineName,
                CreatedOn = DateTime.UtcNow,
                ApplicationName = ApplicationName
            };
        });
    }

}