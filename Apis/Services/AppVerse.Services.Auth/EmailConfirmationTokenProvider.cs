

namespace AppVerse.Service.Authentication;

internal class EmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<DataProtectionTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) :
        base(dataProtectionProvider, options, logger)
    {
    }
}