
namespace AppVerse.Api.Conference.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HostController : ControllerBase
{
    private readonly IMediator mediator;

    public HostController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<string>GetAgenda()
    {
        return "";
    }
}
