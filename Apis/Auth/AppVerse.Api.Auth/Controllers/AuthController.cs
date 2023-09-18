
using AppVerse.Models;

namespace AppVerse.Api.Auth.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet]
    public async Task<List<string>> GetEnvResponse(CancellationToken cancellationToken)
    {
        List<string> resp = new List<string>();
        var envVars = Environment.GetEnvironmentVariables();
        foreach (var envVar in envVars)
            resp.Add(envVar.ToString());

        return resp;
    }

    [HttpGet]
    public async Task<object> GetSvcResponse(string path, CancellationToken cancellationToken)
    {
        HttpClient client = new();
        var response = await client.GetAsync(path, cancellationToken);
        return response.Content;
    }

    [HttpPost]
    public Task Register([FromBody] CreateProfileModel model, CancellationToken cancellationToken) => mediator.Send(
        new CreateProfile(
            model.FirstName,
            model.LastName,
            model.Password,
            model.ConfirmPassword,
            model.ReferalCode,
            model.EmailId,
            LoginSourceEnum.AppVerse), cancellationToken);

    [HttpPost]
    public Task<AuthenticationResponseModel> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
   => mediator.Send(new LoginappverseUser(model.UserName, model.Password), cancellationToken);

    [HttpPost]
    public Task ResendConfirmationEmail([FromBody] EmailConfirmationModel model, CancellationToken cancellationToken)
        => mediator.Send(new VerifyEmailCommand(model.UserEmail), cancellationToken);

    [HttpPost]
    public async Task ConfirmEmailAddress([FromBody] EmailConfirmationModel model, CancellationToken cancellationToken) =>
    await mediator.Send(new EmailConfirmation(model.UserEmail, model.Token), cancellationToken);


    [HttpPost]
    public Task<AuthenticationResponseModel> RefreshTokenAsync(RefreshTokenModel request, CancellationToken cancellationToken)
        => mediator.Send(new OnRefreshToken(request.RefreshToken), cancellationToken);


    [HttpPost]
    public Task ForgotPassword(ForgotPasswordModel model, CancellationToken cancellationToken) =>
        mediator.Send(new ForgotPasswordEmail(model.Email), cancellationToken);


    [HttpPost]
    public Task ResetPassword(ResetPasswordModel model, CancellationToken cancellationToken) => mediator.Send(new ResetAndChangePassword(model.Email, model.Token, model.Password, model.ConfirmPassword), cancellationToken);



    [HttpPost]
    public Task<UserToken> IsEmailTaken([FromBody] EmailModel model, CancellationToken cancellationToken) => mediator.Send(new IsEmailValid(model.UserEmail), cancellationToken);

    [HttpPost]
    public Task<UserToken> IsIdTaken([FromBody] IdRequestModel model, CancellationToken cancellationToken) => mediator.Send(new IsIdValid(model.UserId), cancellationToken);

    
  
    [HttpPost("Password")]
    public Task ChangePassword(ChangePassword request, CancellationToken cancellationToken) => mediator.Send(request, cancellationToken);


}
