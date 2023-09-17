using AppVerse.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppVerse.Infrastructure.Behavior;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;
    private readonly IIdentityService identityService;
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IIdentityService identityService)
    {
        this.logger = logger;
        this.identityService = identityService;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            if (cancellationToken.IsCancellationRequested)
                return default;
            return await next();
        }
        catch (Exception e)
        {
            if (cancellationToken.IsCancellationRequested)
                return default;
            if (e.Message == "The operation was canceled.")
                return default;
           
            throw;
        }
    }
}