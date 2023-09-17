namespace Microsoft.Extensions.DependencyInjection;

public static class AppverDependencies
{
    public static void AddAppVerseDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAppAuthentication(configuration);
        services.AddAppVerseDbContext(configuration);
        services.AddApiControllers();
    }

    internal static void ConfigureAntiforgery(this IApplicationBuilder app)
    {
        app.Use(next => context =>
        {
            var path = context.Request.Path.Value;

            if (path.Contains("/api"))
            {

                var antiforgery = context.RequestServices.GetService<IAntiforgery>();
                var tokens = antiforgery.GetAndStoreTokens(context);
                context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
            }

            return next(context);
        });
    }
    internal static void AddApiControllers(this IServiceCollection services)
    {
        services.AddAntiforgery(options => { options.HeaderName = "X-XSRF-TOKEN"; });
        services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 268435456; });
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services
            .AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };
                    var result = new BadRequestObjectResult(problemDetails);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                    return result;
                };
            });
    }
    public static void ConfigureApp(this IApplicationBuilder app)
    {
        app.UseResponseCompression();
        app.UseSerilogRequestLogging();

        app.ConfigureExceptionHandler();

        app.ConfigureAntiforgery();

        app.UseHttpsRedirection();

        // app.UseRequestMiddleware();
        app.UseCookiePolicy();

        app.UseCors(builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseResponseCaching();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
    }

}
