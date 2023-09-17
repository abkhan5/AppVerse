using AppVerse.DTO;
using AppVerse.Services;
using FluentValidation;

namespace EveryEng;


public static class EntityValidator
{
    public static IRuleBuilderOptions<T, List<FilesDto>> FilesAreNotEmpty<T>(this IRuleBuilder<T, List<FilesDto>> ruleBuilder)
        => ruleBuilder.
        Must(x => x != null && x.Any() && x.All(xitem => xitem.File != null && xitem.File.Length > 0)).
        WithMessage("{PropertyName} should be not empty");

    public static IRuleBuilderOptions<T, string> IsOwnedAsync<TEntity, T>(this IRuleBuilder<T, string> ruleBuilder, IRepository repository, IIdentityService identity) where TEntity : BaseDto
        => ruleBuilder.NotNull().NotEmpty().
        MustAsync((id, cancellationToken) =>
        repository.IsOwned<TEntity>(id, identity.GetUserIdentity(), cancellationToken)).
        WithErrorCode(EveryEngErrorRegistry.UnAuthorizedError).
        WithMessage(EveryEngErrorRegistry.ErrorCatalog[EveryEngErrorRegistry.UnAuthorizedError]);

    public static IRuleBuilderOptions<T, TEntity> IsNotNullAndOwnedAsync<TEntity, T>(this IRuleBuilder<T, TEntity> ruleBuilder, IRepository repository, IIdentityService identityService) where TEntity : BaseDto
        => ruleBuilder.NotNull().NotEmpty().
        MustAsync((entity, cancellationToken) => repository.IsOwned<TEntity>(entity.Id, identityService.GetUserIdentity(), cancellationToken)).
        WithErrorCode(EveryEngErrorRegistry.UnAuthorizedError).
        WithMessage(EveryEngErrorRegistry.ErrorCatalog[EveryEngErrorRegistry.UnAuthorizedError]);


    public static IRuleBuilderOptions<T, string> EntityExistsAsync<T, TEntity>(this IRuleBuilder<T, string> ruleBuilder, IRepository repository) where TEntity : BaseDto
        => ruleBuilder.
        MustAsync(async (id, cancellationToken) =>
        await repository.Get<TEntity>(id, cancellationToken) != null).
        WithErrorCode(EveryEngErrorRegistry.ResourceNotFound).
        WithMessage(EveryEngErrorRegistry.ErrorCatalog[EveryEngErrorRegistry.ResourceNotFound]);
}
