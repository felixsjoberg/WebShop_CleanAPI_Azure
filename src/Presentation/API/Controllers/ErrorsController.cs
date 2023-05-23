using Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            UserExistsException => (StatusCodes.Status400BadRequest, "User already exists."),
            EmailExistsException => (StatusCodes.Status400BadRequest, "Email already exists."),
            NotFoundException => (StatusCodes.Status404NotFound, "Resource not found."),
            NotEnoughStockException => (StatusCodes.Status400BadRequest, "Not enough stock."),
            EmptySecretKeyException => (StatusCodes.Status400BadRequest, "Secret key cannot be empty."),
            ProductNameExistException => (StatusCodes.Status400BadRequest, "Product name already exists."),
            OutOfCategoryRange => (StatusCodes.Status400BadRequest, "Category does not exist."),
            ProductOnOrdersException => (StatusCodes.Status400BadRequest, "Product is on orders or does not exist."),
            UserAccountAlreadyGotCustomerException => (StatusCodes.Status400BadRequest, "User account already has a customer bound to it."),
            PasswordValidationException => (StatusCodes.Status400BadRequest, "Passwords must have at least one non alphanumeric character, contain at least one digit ('0'-'9') and have at least one uppercase ('A'-'Z')."),
            _ => (StatusCodes.Status500InternalServerError, "An error occurred.") // default response for unhandled exceptions
        };
        return Problem(statusCode: statusCode, title: message);
    }
}
