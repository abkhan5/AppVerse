
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Net.Http.Headers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature.Error;

                // https://tools.ietf.org/html/rfc7807#section-3.1
                var problemDetails = new ProblemDetails
                {
                    Instance = context.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                    //Type = $"https://example.com/problem-types/{exception.GetType().Name}",
                };
                Dictionary<string, IEnumerable<string>> validationErrors = null;
                switch (exception)
                {               
                  

                    case Exception generalException:
                        validationErrors = generalException.FormatValidationException();
                        break;
                }

                problemDetails.Extensions["errors"] = validationErrors;
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
                {
                    NoCache = true
                };
                await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
            });
        });
    }




    private static Dictionary<string, IEnumerable<string>> FormatValidationException(this Exception appverseException)
    {
        return new Dictionary<string, IEnumerable<string>>
        {
            [""] = new List<string> { appverseException.Message }
        };
    }

}