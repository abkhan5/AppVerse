using System.Net;

namespace AppVerse.Services;

public interface IIdentityService
{
    public string GetUserIdentity();
    public string GetUserEmail();
    IPAddress GetIpAddress();
    public IPAddress GetLocalAddress();
}