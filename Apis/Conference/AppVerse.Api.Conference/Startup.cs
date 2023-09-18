


using AppVerse.Domain.Conference.Commands;
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
  

        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public Type[] GetAppTypes() =>    
       new Type[]
        {
            typeof(CreateConference),
            typeof(Startup),
            typeof(UserDomainEventHandler)
        };
    
}