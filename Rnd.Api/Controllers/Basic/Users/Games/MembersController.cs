using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Controllers.Basic.Users.Games;

[ApiController]
[Route("basic/users/{userId:guid}/games/{gameId:guid}/[controller]")]
public class MembersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<MemberModel>> Get(Guid userId, Guid gameId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<MemberModel>>> List(Guid userId, Guid gameId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("[action]/{id:guid}")]
    public Task<ActionResult> Exist(Guid userId, Guid gameId, Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("[action]")]
    public Task<ActionResult> ValidateForm(Guid userId, Guid gameId, [FromQuery] MemberFormModel form, bool insert = false)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<MemberModel>> Invite(Guid userId, Guid gameId, MemberFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<MemberModel>> Edit(Guid userId, Guid gameId, Guid id, MemberFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<MemberModel>> Kick(Guid userId, Guid gameId, Guid id)
    {
        throw new NotImplementedException();
    }
}