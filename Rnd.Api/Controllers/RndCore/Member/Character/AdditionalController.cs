using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class AdditionalController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Set(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
}