using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.ResourceEffect;

namespace Rnd.Api.Controllers.Basic.Member.Character.Effects;

[ApiController]
[Route("basic/members/{memberId:guid}/characters/{characterId:guid}/effects/{effectId:guid}/[controller]")]
public class ResourceEffectsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<ResourceEffectModel>> Get(Guid memberId, Guid effectId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<ResourceEffectModel>>> List(Guid memberId, Guid effectId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<ResourceEffectModel>> Add(Guid memberId, Guid effectId, ResourceEffectAddModel add)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<ResourceEffectModel>> Edit(Guid memberId, Guid effectId, ResourceEffectEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<ResourceEffectModel>> Remove(Guid memberId, Guid effectId, Guid id)
    {
        throw new NotImplementedException();
    }
}