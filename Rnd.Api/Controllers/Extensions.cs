using Microsoft.AspNetCore.Mvc;
using Rnd.Results;

namespace Rnd.Api.Controllers;

public static class Extensions
{
    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Success 
            ? new OkObjectResult(new {Data = result.Get(), result.Message}) 
            : new ObjectResult(new {result.Message}) {StatusCode = (int) result.Status};
    }
}