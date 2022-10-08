using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Controllers.Basic.Game;

[ApiController]
[Route("basic/game/{gameId:guid}/[controller]")]
public class MembersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<MemberModel>> Get(Guid gameId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<MemberModel>>> List(Guid gameId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<MemberModel>> Invite(Guid gameId, MemberInviteModel invite)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<MemberModel>> Edit(Guid gameId, MemberEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<MemberModel>> Kick(Guid gameId, Guid id)
    {
        throw new NotImplementedException();
    }
}