using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.RndCore.Leveling;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class LevelingController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<LevelingModel>> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<LevelingModel>> Set(Guid memberId, Guid characterId, Guid id, LevelingFormModel form)
    {
        throw new NotImplementedException();
    }
}