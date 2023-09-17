namespace AppVerse.Services.AzureOtp;

public class AzureOtpServices : IOtpService
{
    public Task<bool> IsOtpValid(string otp, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendOtp(UInt128 phoneNumber, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}