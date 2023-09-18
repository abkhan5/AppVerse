
namespace AppVerse.Api.Conference.Controllers;

[ApiController]
[Route("[controller]")]
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
