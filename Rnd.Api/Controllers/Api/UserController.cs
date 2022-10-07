using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Login()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> Register()
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