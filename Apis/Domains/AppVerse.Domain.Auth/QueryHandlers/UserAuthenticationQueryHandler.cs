

using AppVerse.Models;

namespace AppVerse.Domain.Authentication.QueryHandlers;
public class UserAuthenticationQueryHandler :
    IQueryHandler<OnRefreshToken, AuthenticationResponseModel>,
    IQueryHandler<LoginappverseUser, AuthenticationResponseModel>,
    IQueryHandler<ValidateOtp, AuthenticationResponseModel>,
    IQueryHandler<LoginRequest, string>
{
    private readonly IUserAuthenticationService authenticationService;
    private readonly DbContext context;
    private readonly IMediator mediator;
    private readonly IDistributedCacheRepository distributedCacheRepository;
    private readonly IUniqueCodeGeneratorService uniqueCodeGeneratorService;
    public UserAuthenticationQueryHandler(DbContext context, IUserAuthenticationService authenticationService,
        IMediator mediator, IDistributedCacheRepository distributedCacheRepository, IUniqueCodeGeneratorService uniqueCodeGeneratorService)
    {
        this.context = context;
        this.authenticationService = authenticationService;
        this.mediator = mediator;
        this.distributedCacheRepository = distributedCacheRepository;
        this.uniqueCodeGeneratorService = uniqueCodeGeneratorService;
    }


    public async Task<AuthenticationResponseModel> Handle(LoginappverseUser request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.FindByNameAsync(request.UserName);
        await mediator.Publish(new UserDomainEvent("Login", "EmailLogin", user.Id), cancellationToken);
        return await GetAuthenitcationResponse(user, cancellationToken);
    }


    public async Task<AuthenticationResponseModel> Handle(OnRefreshToken request, CancellationToken cancellationToken)
    {
        var refreshToken = await context.Set<RefreshToken>().Include(item => item.Profile)
            .SingleAsync(u => u.Token == request.RefreshToken, cancellationToken);
        refreshToken.Invalidated = true;
        refreshToken.ExpiryDate = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);
        var user = refreshToken.Profile;
        return await GetAuthenitcationResponse(user, cancellationToken);
    }

    public async Task<AuthenticationResponseModel> Handle(ValidateOtp request, CancellationToken cancellationToken)
    {
        var user = await context.Set<AppVerseUser>().FirstOrDefaultAsync(item => item.PhoneNumber == request.PhoneNumber, cancellationToken);
        await mediator.Publish(new UserDomainEvent("Login", "OtpLogin", user.Id), cancellationToken);
        return await GetAuthenitcationResponse(user, cancellationToken);
    }

    public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var requestKey = uniqueCodeGeneratorService.GetUniqueCode(request.UserLoginInput);
        await distributedCacheRepository.SetEntityAsync(request.UserLoginInput, requestKey, new TimeSpan(0, 15, 0), cancellationToken);
        return requestKey;
    }

    private async Task<AuthenticationResponseModel> GetAuthenitcationResponse(AppVerseUser user, CancellationToken cancellationToken)
    {
        await authenticationService.ResetAccessFailedCountAsync(user);
        var principal = await authenticationService.CreatePrincipalAsync(user);
        return await authenticationService.GenerateToken(principal, user, cancellationToken);
    }
}