using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api.Member.Character;

[ApiController]
[Route("api/member/{memberId:guid?}/character/{characterId:guid?}/[controller]/[action]")]
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