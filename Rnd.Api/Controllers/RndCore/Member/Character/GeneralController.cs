using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.RndCore.General;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class GeneralController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<GeneralModel>> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<GeneralModel>> Set(Guid memberId, Guid characterId, GeneralSetModel set)
    {
        throw new NotImplementedException();
    }
}