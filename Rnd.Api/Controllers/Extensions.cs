using Microsoft.AspNetCore.Mvc;
using Rnd.Results;

namespace Rnd.Api.Controllers;

public static class Extensions
{
    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Success 
            ? new OkObjectResult(result.Get()) 
            : new ObjectResult(result.Get()) {StatusCode = (int) result.Status};
    }
}