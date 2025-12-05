using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IRequestExecutor request) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterUserCommand command) => Ok(await request.ExecuteAsync<RegisterUserCommand, Guid>(command));

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command)
    {
        var token = await request.ExecuteAsync<LoginUserCommand, string>(command);
        return Ok(new { Token = token });
    }
}
