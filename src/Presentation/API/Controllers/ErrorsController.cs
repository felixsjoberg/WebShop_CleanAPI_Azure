using Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Error()
    {

        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            InvalidLoginCombination => (StatusCodes.Status400BadRequest, "Invalid username or password."),
            _ => (StatusCodes.Status500InternalServerError, "An error occurred.") // default response for unhandled exceptions
        };
        return Problem(statusCode: statusCode, title: message);
    }
}

