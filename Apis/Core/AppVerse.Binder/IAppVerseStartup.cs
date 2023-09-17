namespace Microsoft.Extensions.DependencyInjection;

public interface IAppVerseStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    public void ConfigureApplication(IApplicationBuilder app)
    {
        app.ConfigureApp();
    }
}
