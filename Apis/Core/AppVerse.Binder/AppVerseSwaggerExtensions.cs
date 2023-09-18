using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection;

public static class AppVerseSwaggerExtensions
{
    public static void AddSwaggerUI(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            c.DocumentTitle = $"API for {ProgramStartupExtensions.ApplicationName}";
            c.DefaultModelsExpandDepth(0);
            // c.RoutePrefix = string.Empty;
        });
    }

    public static void AddSwagerDefinition(this IServiceCollection services, string applications)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = applications, Version = "v1" });
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //c.IncludeXmlComments(xmlPath, true);
            c.EnableAnnotations();
            c.SchemaFilter<CustomSchemaFilters>();
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });

            // add Basic Authentication
            var basicSecurityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                Reference = new OpenApiReference
                {
                    Id = "BasicAuth",
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { basicSecurityScheme, Array.Empty<string>() }
            });
        });
    }
}
public class CustomSchemaFilters : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var excludeProperties = new[] { "CorrelationId" };

        foreach (var prop in excludeProperties)
            if (schema.Properties.ContainsKey(prop))
                schema.Properties.Remove(prop);
    }
}
