namespace AppVerse.Service.Authentication;

internal static class OnAuthenticationServiceExtensions
{
    public const string AuthTokenKey = "authtoken~";

    internal static void AddOnAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // configure strongly typed settings objects
        var appSettingsSection = configuration.GetSection(JwtSettings.JwtOptionsName);
        services.Configure<JwtSettings>(appSettingsSection);

        // configure jwt authentication
        var appSettings = appSettingsSection.Get<JwtSettings>();

        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                var aut = x.Authority;
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret))
                };

                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var cancellationToken = new CancellationTokenSource().Token;
                        var distributedCache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCacheRepository>();
                        var userName = context.Principal.Identity.Name;
                        var cacheKey = AppVerseUserDto.CreateCacheKey(userName);
                        var cachedUser = await distributedCache.GetAsync<AppVerseUserDto>(cacheKey, cancellationToken);
                        var userExist = cachedUser != null;
                        if (!userExist)
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserStore<AppVerseUser>>();
                            var user = new AppVerseUserDto(await userService.FindByNameAsync(userName, cancellationToken));
                            userExist = user != null;
                            if (userExist)
                                await distributedCache.SetLocalEntityAsync(cacheKey, user, new TimeSpan(1, 0, 0), cancellationToken);

                        }
                        if (!userExist)
                            context.Fail("Unauthorized");

                        await distributedCache.RefreshCache(cacheKey, cancellationToken);
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/cop") || path.StartsWithSegments("/chat") ||
                            path.StartsWithSegments("/appverseuser"))
                            // Read the token out of the query string
                            if (!string.IsNullOrEmpty(accessToken))
                                context.Token = accessToken;
                        return Task.CompletedTask;
                    }
                };
            })
            .AddGoogle(options =>
            {
                var googleCredentials = new GoogleLoginCredentialsSettings();
                configuration.GetSection(GoogleLoginCredentialsSettings.GoogleLoginCredentialsOptions)
                    .Bind(googleCredentials);
                options.ClientId = googleCredentials.ClientId;
                options.ClientSecret = googleCredentials.ClientSecret;
                //this function is get user google profile image
                options.Scope.Add("profile");
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "UserId");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "EmailAddress", ClaimValueTypes.Email);
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "Name");

                options.Events.OnCreatingTicket = context =>
                {
                    var picture = context.User.GetProperty("picture").GetString();
                    context.Identity.AddClaim(new Claim("picture", picture));
                    return Task.CompletedTask;
                };
            });

        ;
    }
}