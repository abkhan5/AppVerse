
namespace AppVerse.Domain.Authentication.Queries;

public record GetPermission(EveryEngDomain Domain) : IQuery<PermissionDto>;