using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class BackstoryController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Show()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Set()
    {
        throw new NotImplementedException();
    }
}