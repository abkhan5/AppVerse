

namespace AppVerse.Domain.Authentication.QueryHandlers;
public class UserQueryHandler : IQueryHandler<IsEmailValid, UserToken>
    , IQueryHandler<IsIdValid, UserToken>
{
    private readonly IUserAuthenticationService appverseUserAuthenticationService;
    private readonly IOtpService otpService;
    public UserQueryHandler(
        IUserAuthenticationService appverseUserAuthenticationService, 
        IOtpService otpService)
    {
        this.appverseUserAuthenticationService = appverseUserAuthenticationService;
        this.otpService = otpService;
    }

    public async Task<UserToken> Handle(IsEmailValid model, CancellationToken cancellationToken)
     => new UserToken(await appverseUserAuthenticationService.CreateToken(model.UserEmail, cancellationToken), false);

    public async Task<UserToken> Handle(IsIdValid model, CancellationToken cancellationToken)
    {
        var isPhoneNumber = UInt128.TryParse(model.UserId, out UInt128 number);
        if (isPhoneNumber)
            await otpService.SendOtp(number, cancellationToken);

        return new UserToken(await appverseUserAuthenticationService.CreateToken(model.UserId, cancellationToken), isPhoneNumber);
    }

}