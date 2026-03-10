using Ecommerce.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected IActionResult HandleFailure(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Invalid operation.");
        }

        if (result.Error is ValidationError validationError)
        {
            return ValidationProblem(new ValidationProblemDetails(validationError.ValidationErrors)
            {
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest,
                Detail = validationError.Message,
                Extensions = { ["errorCode"] = validationError.Code }
            });
        }

        return Problem(
            title: "Business logic error",
            statusCode: StatusCodes.Status400BadRequest,
            detail: result.Error.Message,
            extensions: new Dictionary<string, object?>
            {
                { "errorCode", result.Error.Code }
            }
        );
    }
}