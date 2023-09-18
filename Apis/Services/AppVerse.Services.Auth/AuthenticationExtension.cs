


namespace Microsoft.Extensions.DependencyInjection;
public static class AuthenticationExtension
{
    public static void AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtOptionsName));

        services.Configure<GoogleLoginCredentialsSettings>(
         configuration.GetSection(GoogleLoginCredentialsSettings.GoogleLoginCredentialsOptions));

        services.Configure<GoogleAuthenticationOptions>(
            configuration.GetSection(GoogleAuthenticationOptions.OptionName));

        services.AddTransient<IIdentityService, IdentityService>();
        services.AddTransient<IUserStore<AppVerseUser>, ProfileUserStore>();
        services.AddTransient<JwtTokenGenerator>();
        services.AddTransient<GoogleLoginServices>();
        services.AddTransient<SignInManager<AppVerseUser>>();
        services.AddTransient<IUniqueCodeGeneratorService, GuidUniqueCodeGeneratorService>();
        services.AddTransient<IUserAuthenticationService, AppUserAuthenticationService>();
        services.AddTransient<IUserPasswordStore<AppVerseUser>, ProfileUserStore>();
        services.AddIdentity<AppVerseUser, IdentityRole>(options =>
            {
                //TODO: see if it works
                // options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconf";
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 4;
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppVerseDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<AppVerseUser>>("emailconf")
            .AddPasswordValidator<DoesNotContainPasswordValidator<AppVerseUser>>();

        services.AddScoped<IUserClaimsPrincipalFactory<AppVerseUser>, ProfileClaimsPrincipalFactory>();

        services.Configure<DataProtectionTokenProviderOptions>(options =>
        {
            options.TokenLifespan = TimeSpan.FromDays(1);
        });
        services.Configure<EmailConfirmationTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(2));

        services.AddTransient<IUserStore<AppVerseUser>, ProfileUserStore>();
        services.AddTransient<IUserPasswordStore<AppVerseUser>, ProfileUserStore>();
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.JwtOptionsName));
        services.AddOnAuthentication(configuration);
        services.ConfigureClaims();
        //  services.AddScoped<IAuthorizationMiddlewareResultHandler, AppVerseUserAuthorizationHandler>();
    }

    private static void ConfigureClaims(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireClaim("Admin"));
            options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        });
    }
}