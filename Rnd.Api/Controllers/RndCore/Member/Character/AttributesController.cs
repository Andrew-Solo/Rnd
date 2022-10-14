using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.RndCore.Attributes;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class AttributesController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<AttributesModel>> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<AttributesModel>> Set(Guid memberId, Guid characterId, Guid id, AttributesFormModel form)
    {
        throw new NotImplementedException();
    }
}