using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api.Member.Character;

[ApiController]
[Route("api/member/{memberId:guid?}/character/{characterId:guid?}/[controller]/[action]")]
public class SkillsController : ControllerBase
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