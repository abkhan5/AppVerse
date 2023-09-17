using AppVerse.Infrastructure.Behavior;
using FluentValidation;
using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class MediatRExtensions
{
    public static void ConfigureMediator(this IServiceCollection services, params Type[] types)
    {
        var assemblies = types.Select(item => item.Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies.ToArray()));
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }
}