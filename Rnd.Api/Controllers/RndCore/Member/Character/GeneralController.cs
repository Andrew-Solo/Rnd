using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.RndCore.General;

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
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<GeneralModel>> Set(Guid memberId, Guid characterId, Guid id, GeneralFormModel form)
    {
        throw new NotImplementedException();
    }
}