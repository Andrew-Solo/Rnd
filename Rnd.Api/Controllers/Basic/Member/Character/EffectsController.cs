using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.Basic.Effect;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
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
    public Task<ActionResult<EffectModel>> Add(Guid memberId, Guid characterId, EffectAddModel add)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<EffectModel>> Edit(Guid memberId, Guid characterId, EffectEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<EffectModel>> Remove(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
}