


[assembly: ApiController]

public class Startup : IAppVerseStartup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtOptionsName));
        services.AddHttpClient();     
        services.AddAppVerseDependencies(configuration);     

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void ConfigureApplication(IApplicationBuilder app)
    {
        app.ConfigureApp();    
    }

    public Type[] GetAppTypes() =>    
       new Type[]
        {
            typeof(CreateConference),
            typeof(Startup),
            typeof(UserDomainEventHandler)
        };
    
}