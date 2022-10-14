using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.RndCore.Additional;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class AdditionalController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<AdditionalModel>> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<AdditionalModel>> Set(Guid memberId, Guid characterId, Guid id, AdditionalFormModel form)
    {
        throw new NotImplementedException();
    }
}