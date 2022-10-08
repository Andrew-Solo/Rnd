using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/member/{memberId:guid?}/character/{characterId:guid?}/[controller]")]
public class EffectsController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> List()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> Add()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Edit()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public Task<IActionResult> Remove()
    {
        throw new NotImplementedException();
    }
}