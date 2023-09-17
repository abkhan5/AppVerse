using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Runtime.CompilerServices;

namespace AppVerse.Infrastructure.Behavior;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;
    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return default;

        if (!validators.Any())
            return await next();

        List<ValidationFailure> messagesErrors = new();


        await foreach (var errors in GetFailures(request, cancellationToken))
            messagesErrors.AddRange(errors);


        if (messagesErrors.Any())
            throw new ValidationException(messagesErrors);

        return await next();
    }

    private async IAsyncEnumerable<List<ValidationFailure>> GetFailures(TRequest request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var validator in validators)
        {
            var response = await validator.ValidateAsync(request, cancellationToken);
            if (response != null && response.Errors.Any())
                yield return response.Errors;
        }
    }
}