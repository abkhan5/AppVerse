

namespace AppVerse.Service.Authentication;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public string GetUserIdentity()
        => _context?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;


    public IPAddress? GetIpAddress()
        => _context?.HttpContext.Connection.RemoteIpAddress;
    public IPAddress? GetLocalAddress()
        => _context?.HttpContext.Connection.LocalIpAddress;

    public string GetUserEmail()
        => _context?.HttpContext.User.Claims.FirstOrDefault()?.Subject.Name;

}