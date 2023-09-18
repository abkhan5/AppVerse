namespace Microsoft.Extensions.DependencyInjection;

public interface IAppVerseStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    public Type[] GetAppTypes();

    public void ConfigureApplication(IApplicationBuilder app)
    {
        app.ConfigureApp();
    }
}
