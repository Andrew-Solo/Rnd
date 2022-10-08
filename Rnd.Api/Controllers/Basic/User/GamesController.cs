using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.Game;

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
    public Task<ActionResult<GameModel>> Create(GameCreateModel create, Guid userId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<GameModel>> Edit(GameEditModel edit, Guid userId)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<GameModel>> Delete(Guid userId, Guid id)
    {
        throw new NotImplementedException();
    }
}