using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api.Game;

[ApiController]
[Route("api/game/{gameId:guid?}/[controller]/[action]")]
public class MembersController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> List()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> Invite()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Edit()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public Task<IActionResult> Kick()
    {
        throw new NotImplementedException();
    }
}