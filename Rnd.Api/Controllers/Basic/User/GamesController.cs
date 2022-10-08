using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.Basic.Game;

namespace Rnd.Api.Controllers.Basic.User;

[ApiController]
[Route("basic/user/{userId:guid?}/[controller]")]
public class GamesController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<GameModel>> Get(Guid userId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<GameModel>>> List(Guid userId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<GameModel>> Create(Guid userId, GameCreateModel create)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<GameModel>> Edit(Guid userId, GameEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<GameModel>> Delete(Guid userId, Guid id)
    {
        throw new NotImplementedException();
    }
}