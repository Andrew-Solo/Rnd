using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.Basic.ParameterEffect;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/member/{memberId:guid}/effect/{effectId:guid}/[controller]")]
public class ParameterEffectsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<ParameterEffectModel>> Get(Guid memberId, Guid effectId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<ParameterEffectModel>>> List(Guid memberId, Guid effectId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<ParameterEffectModel>> Add(Guid memberId, Guid effectId, ParameterEffectAddModel add)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<ParameterEffectModel>> Edit(Guid memberId, Guid effectId, ParameterEffectEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<ParameterEffectModel>> Remove(Guid memberId, Guid effectId, Guid id)
    {
        throw new NotImplementedException();
    }
}