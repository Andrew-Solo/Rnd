using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.Resource;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/members/{memberId:guid}/characters/{characterId:guid}/[controller]")]
public class ResourcesController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<ResourceModel>> Get(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<ResourceModel>>> List(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<ResourceModel>> Add(Guid memberId, Guid characterId, ResourceFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<ResourceModel>> Edit(Guid memberId, Guid characterId, Guid id, ResourceFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<ResourceModel>> Remove(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
}