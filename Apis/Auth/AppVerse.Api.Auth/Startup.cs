


[assembly: ApiController]

public class Startup : IAppVerseStartup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtOptionsName));
        services.AddHttpClient();      
     
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


    public Type[] GetAppTypes() =>
       new Type[]
        {
            typeof(CreateProfile),
            typeof(Startup),
            typeof(UserDomainEventHandler)
        };
}