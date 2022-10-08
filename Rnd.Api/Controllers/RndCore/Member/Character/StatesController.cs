using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.RndCore.States;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class StatesController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<StatesModel>> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<StatesModel>> Set(Guid memberId, Guid characterId, StatesSetModel set)
    {
        throw new NotImplementedException();
    }
}