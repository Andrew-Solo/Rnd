using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Basic.Member.Character.Effect;

[ApiController]
[Route("basic/member/{memberId:guid?}/character/{characterId:guid?}/effect/{effectId:guid?}/[controller]")]
public class EffectController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Show()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("[action]")]
    public Task<IActionResult> AddParameterEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("[action]")]
    public Task<IActionResult> AddResourceEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("[action]")]
    public Task<IActionResult> EditParameterEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("[action]")]
    public Task<IActionResult> EditResourceEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public Task<IActionResult> RemoveSubeffect()
    {
        throw new NotImplementedException();
    }
}