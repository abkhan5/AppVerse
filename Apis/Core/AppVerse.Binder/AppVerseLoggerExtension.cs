using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.IdentityModel.Logging;
using Serilog.Events;
using Serilog.Exceptions;
using System.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public class AppVerseLoggerExtension
{
    public static void InitializeLogger(IConfigurationBuilder appConfig, string applicationName)
    {
        var config = appConfig.Build();
        var useMachineName = config.GetValue("useMachineName", false);
        var loglevel = useMachineName ? LogEventLevel.Debug : LogEventLevel.Error;
        //var elastisearchUrl = config.GetValue("elastisearchUrl", "http://localhost:9200");
        
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithProcessName()
            .Enrich.WithProcessId()
            .Enrich.WithProperty("Application", applicationName)
            .MinimumLevel.Override("Microsoft", loglevel)
            .MinimumLevel.Override("System", loglevel)
            .MinimumLevel.Override("Microsoft.AspNetCore", loglevel)
            .Filter.ByExcluding(logEvent => logEvent.Exception != null && logEvent.Exception.GetType() == typeof(TaskCanceledException));

        //.WriteTo.Console(loglevel);
        IdentityModelEventSource.ShowPII = true;
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        logger = logger.WriteTo.Console(loglevel);

        //.WriteTo.GrafanaLoki();
        // .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
        //{
        //    AutoRegisterTemplate = true,
        //    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
        //});
        //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elastisearchUrl))
        //{
        //    IndexFormat = $"{ApplicationName}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        //    AutoRegisterTemplate = true,
        //    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
        //    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
        //});


        Log.Logger = logger.CreateLogger();
    }

}
