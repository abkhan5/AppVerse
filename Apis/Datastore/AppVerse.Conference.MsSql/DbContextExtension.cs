namespace AppVerse.Conference.MsSql;

public static class DbContextExtension
{
    public static void AddEveryEngDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AzureSqlStoreSettings>(configuration.GetSection(AzureSqlStoreSettings.AzureSqlStoreOptions));
        services.AddScoped<DbContext, AppVerseDbContext>();
        services.AddDbContextPool<AppVerseDbContext>((sp, options) =>
        {
            var sqlStorSettings = sp.GetService<IOptions<AzureSqlStoreSettings>>();
            options.UseSqlServer(sqlStorSettings.Value.AzureSqlConnectionString,
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(AzureSqlStoreSettings).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure();
                });
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
        services.AddScoped<DbContext, AppVerseDbContext>();
    }
}