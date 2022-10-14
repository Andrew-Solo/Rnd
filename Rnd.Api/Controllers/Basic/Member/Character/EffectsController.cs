using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.Effect;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/members/{memberId:guid}/characters/{characterId:guid}/[controller]")]
public class EffectsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<EffectModel>> Get(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<EffectModel>>> List(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<EffectModel>> Add(Guid memberId, Guid characterId, EffectFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<EffectModel>> Edit(Guid memberId, Guid characterId, Guid id, EffectFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<EffectModel>> Remove(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
}