using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Basic.Game;

[ApiController]
[Route("basic/game/{gameId:guid?}/[controller]")]
public class MembersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<IActionResult> Get(Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<IActionResult> List(Guid gameId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> Invite(Guid gameId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Edit(Guid gameId)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<IActionResult> Kick(Guid gameId, Guid id)
    {
        throw new NotImplementedException();
    }
}