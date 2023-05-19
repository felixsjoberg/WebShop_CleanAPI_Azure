using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Contracts.Authentication;
using MapsterMapper;
using Application.Authentication.Queries.Login;
using Application.Authentication.Commands.Register;

namespace Presentation.API.Controllers;

[Route("auth")]
[ApiController]
[AllowAnonymous]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        // 1. map request with query
        var command = _mapper.Map<RegisterCommand>(request);
        
        // 2. send query to mediator
        var result = await _mediator.Send(command);

        // 3. map result with response
        var response = _mapper.Map<AuthenticationResponse>(result);

        return Ok(response);
    }
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        // 1. map request with query
        var query = _mapper.Map<LoginQuery>(request);

        // 2. send query to mediator
        var result = await _mediator.Send(query);

        // 3. map result with response
        var response = _mapper.Map<AuthenticationResponse>(result);

        return Ok(response);
    }
}
