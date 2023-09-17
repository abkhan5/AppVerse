


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

[assembly: ApiController]

public class Startup : IAppVerseStartup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtOptionsName));
        services.AddHttpClient();

        services.AddAutoMapper(
            typeof(Startup));

        services.Configure<GoogleLoginCredentialsSettings>(
            configuration.GetSection(GoogleLoginCredentialsSettings.GoogleLoginCredentialsOptions));



        services.ConfigureMediator(typeof(Startup),
            typeof(CreateProfile),
            typeof(IsEmailValid),
            typeof(UserDomainEventHandler));
        var assemblyNames = new List<Assembly>
        {
            typeof(EmailConfirmationValidation).Assembly
        };
        services.AddValidatorsFromAssemblies(assemblyNames);

        services.Configure<GoogleAuthenticationOptions>(
            configuration.GetSection(GoogleAuthenticationOptions.OptionName));

        services.AddAppVerseDependencies(configuration);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth", Version = "v1" });
            c.EnableAnnotations();
        });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void ConfigureApplication(IApplicationBuilder app)
    {
        app.ConfigureApp();
        app.UseOAuthMiddleware();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            c.DocumentTitle = $"API for Auth";
            c.DefaultModelsExpandDepth(0);
            c.RoutePrefix = string.Empty;
        });
    }
}