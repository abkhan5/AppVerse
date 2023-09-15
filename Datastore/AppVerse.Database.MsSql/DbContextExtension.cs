

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppVerse.Database.MsSql;

public static class DbContextExtension
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureSqlStoreSettings>(configuration.GetSection(AzureSqlStoreSettings.AzureSqlStoreOptions));
        services.AddScoped<DbContext, AppVerseDbContext>();
        services.AddDbContextPool<AppVerseDbContext>((sp, options) =>
        {
            var sqlStoreSettings = sp.GetService<IOptions<AzureSqlStoreSettings>>();
            options.UseSqlServer(sqlStoreSettings.Value.ConnectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        services.AddScoped<DbContext, AppVerseDbContext>();
    }
}
