
using AppVerse.Service.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;



[assembly: ApiController]

public class Startup : IAppVerseStartup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtOptionsName));
        services.AddHttpClient();

        services.AddAutoMapper(
            typeof(Startup),
            typeof(EveryEngDbProfiles));

        services.Configure<GoogleLoginCredentialsSettings>(
            configuration.GetSection(GoogleLoginCredentialsSettings.GoogleLoginCredentialsOptions));


        services.Configure<EveryEngServiceSettings>(
            configuration.GetSection(EveryEngServiceSettings.EveryEngServiceOptions));

        services.ConfigureMediator(typeof(Startup),
            typeof(CreateNewProfile),
            typeof(IsEmailValid),
            typeof(UserDomainEventHandler));
        var assemblyNames = new List<Assembly>
        {
            typeof(EmailConfirmationValidation).Assembly,
            typeof(GetDashboard).Assembly
        };
        services.AddValidatorsFromAssemblies(assemblyNames);
        services.Configure<LinkedInAuthenticationOptions>(
            configuration.GetSection(LinkedInAuthenticationOptions.OptionName));
        services.Configure<GoogleAuthenticationOptions>(
            configuration.GetSection(GoogleAuthenticationOptions.OptionName));

        services.AddGlobalDependencies(configuration);
        services.AddBenutzer();
        services.ConfigureCmsExtensions();
        services.AddOtpService();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void ConfigureApplication(IApplicationBuilder app)
    {
        app.ConfigureApp();
        app.UseOAuthMiddleware();
    }
}