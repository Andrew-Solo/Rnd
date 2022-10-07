using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api.Member.Character.Effect;

[ApiController]
[Route("api/member/{memberId:guid?}/character/{characterId:guid?}/effect/{effectId:guid?}/[controller]/[action]")]
public class EffectController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Show()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> AddParameterEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> AddResourceEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> EditParameterEffect()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
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