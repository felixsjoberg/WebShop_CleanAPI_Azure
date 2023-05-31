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
            NotEnoughStockException => (StatusCodes.Status400BadRequest, "One or more items does not have enough stock."),
            NotFoundAddress => (StatusCodes.Status400BadRequest, "Address does not exist."),
            EmptySecretKeyException => (StatusCodes.Status400BadRequest, "Secret key cannot be empty."),
            ProductNameExistException => (StatusCodes.Status400BadRequest, "Product name already exists."),
            ProductAlreadyDeactivated => (StatusCodes.Status400BadRequest, "Product is already inactive."),
            OutOfCategoryRange => (StatusCodes.Status400BadRequest, "Category does not exist."),
            OrderAlreadyShipped => (StatusCodes.Status400BadRequest, "Order is already shipped, cannot update."),
            ProductOnOrdersException => (StatusCodes.Status400BadRequest, "Product is on orders or does not exist."),
            ProductNotFoundException => (StatusCodes.Status400BadRequest, "Product does not exist."),
            InternalProblemWtihCreateOrder => (StatusCodes.Status500InternalServerError, "Internal problem with creating order."),
            CategoryNotFoundException => (StatusCodes.Status400BadRequest, "Category does not exist."),
            ProductNotActiveException => (StatusCodes.Status400BadRequest, "Product is not active."),
            NotFoundOrder => (StatusCodes.Status400BadRequest, "Order does not exist."),
            NoProductsInOrderItems => (StatusCodes.Status400BadRequest, "No products in orderItems."),
            NotFoundCustomer => (StatusCodes.Status400BadRequest, "Customer does not exist."),
            DuplicateProductsInOrderItems => (StatusCodes.Status400BadRequest, "Duplicate products in orderItems, add the quantity to the existing item instead"),
            UserAccountAlreadyGotCustomerException => (StatusCodes.Status400BadRequest, "User account already has a customer bound to it."),
            PasswordValidationException => (StatusCodes.Status400BadRequest, "Passwords must have at least one non alphanumeric character, contain at least one digit ('0'-'9') and have at least one uppercase ('A'-'Z')."),
            _ => (StatusCodes.Status500InternalServerError, "An error occurred.")
        };
        return Problem(statusCode: statusCode, title: message);
    }
}
