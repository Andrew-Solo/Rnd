using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api.User;

[ApiController]
[Route("api/user/{userId:guid?}/[controller]/[action]")]
public class GamesController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> List()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> Create()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Edit()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public Task<IActionResult> Delete()
    {
        throw new NotImplementedException();
    }
}