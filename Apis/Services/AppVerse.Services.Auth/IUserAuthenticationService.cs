namespace AppVerse.Service.Authentication;

public interface IUserAuthenticationService
{
    Task<string> GetLoginLinkAsync();
    Task<AppVerseUser> FindByEmailAsync(string emailId);
    Task<AppVerseUser> FindByNameAsync(string emailId);
    Task<IdentityResult> ConfirmEmailAsync(AppVerseUser user, string token);
    Task<IdentityResult> ResetPasswordAsync(AppVerseUser user, string token, string newPassword);
    Task<IdentityResult> ChangePasswordAsync(AppVerseUser user, string token, string newPassword);
    Task<string> GeneratePasswordResetTokenAsync(AppVerseUser user);
    Task<AppVerseUser> FindByIdAsync(string emailId);
    Task<ClaimsPrincipal> CreatePrincipalAsync(AppVerseUser user);
    Task<bool> CheckPasswordAsync(AppVerseUser user, string password);
    Task<AuthenticationResponseModel> GenerateToken(ClaimsPrincipal principal, AppVerseUser user, CancellationToken cancellation);
    Task<string> GenerateEmailConfirmationTokenAsync(AppVerseUser user);
    Task<IdentityResult> CreateAsync(AppVerseUser user);
    Task<IdentityResult> CreateAsync(AppVerseUser user, string password);
    Task<bool> IsEmailConfirmedAsync(AppVerseUser user);

    Task<string> GenerateSignupLink(AppVerseUser user);
    Task ResetAccessFailedCountAsync(AppVerseUser user);
    Task<IdentityResult> AccessFailedAsync(AppVerseUser user);
    Task<EmailConfirmationDto> GetConfirmationModel(AppVerseUser user);

    Task<bool> IsCreationTokenValid(string emailId, string token, CancellationToken cancellationToken);
    Task<string> CreateToken(string emailId, CancellationToken cancellationToken);
}