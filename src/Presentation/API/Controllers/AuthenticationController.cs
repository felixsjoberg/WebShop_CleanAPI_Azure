using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.Login;

namespace Presentation.API.Controllers;

[Route("auth")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginQuery query)
    {
        return Ok(await _mediator.Send(query));
    }
}
