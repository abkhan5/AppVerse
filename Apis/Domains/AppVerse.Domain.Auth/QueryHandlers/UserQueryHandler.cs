

namespace AppVerse.Domain.Authentication.QueryHandlers;
public class UserQueryHandler :
     IQueryHandler<IsReferralCodeValid, bool>
    , IQueryHandler<IsEmailValid, EveryEngUserToken>
    , IQueryHandler<IsIdValid, EveryEngUserToken>
{
    private readonly IUserAuthenticationService everyEngUserAuthenticationService;
    private readonly IReadCmsRepository readCmsRepository;
    private readonly IUserProfileRepository userProfileRepository;
    private readonly IUserRepository userRepository;
    private readonly ITimeLineRepository timeLineRepository;
    private readonly IIdentityService identityService;
    private readonly IOtpService otpService;
    public UserQueryHandler(IUserRepository userRepository,
        IUserAuthenticationService everyEngUserAuthenticationService, IReadCmsRepository readCmsRepository,
        IUserProfileRepository userProfileRepository,
        ITimeLineRepository timeLineRepository,
        IIdentityService identityService, IOtpService otpService)
    {
        this.userRepository = userRepository;
        this.everyEngUserAuthenticationService = everyEngUserAuthenticationService;
        this.readCmsRepository = readCmsRepository;
        this.userProfileRepository = userProfileRepository;
        this.timeLineRepository = timeLineRepository;
        this.identityService = identityService;
        this.otpService = otpService;
    }






    public async Task<EveryEngUserToken> Handle(IsEmailValid model, CancellationToken cancellationToken)
     => new EveryEngUserToken(await everyEngUserAuthenticationService.CreateToken(model.UserEmail, cancellationToken), false);

    public async Task<EveryEngUserToken> Handle(IsIdValid model, CancellationToken cancellationToken)
    {
        var isPhoneNumber = UInt128.TryParse(model.UserId, out UInt128 number);
        if (isPhoneNumber)
            await otpService.SendOtp(number, cancellationToken);

        return new EveryEngUserToken(await everyEngUserAuthenticationService.CreateToken(model.UserId, cancellationToken), isPhoneNumber);
    }


    public async Task<bool> Handle(IsReferralCodeValid request, CancellationToken cancellationToken) => await userRepository.IsReferralCode(request.ReferralCode, cancellationToken);


    public IAsyncEnumerable<TimeLineDto> Handle(GetUserTimeLine request, CancellationToken cancellationToken) => timeLineRepository.GetTimeLine(identityService.GetUserIdentity(), null, cancellationToken);

}