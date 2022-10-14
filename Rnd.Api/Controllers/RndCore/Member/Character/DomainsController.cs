using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.RndCore.Domains;

namespace Rnd.Api.Controllers.RndCore.Member.Character;

[ApiController]
[Route("rndcore/member/{memberId:guid}/character/{characterId:guid}/[controller]")]
public class DomainsController : ControllerBase
{
    [HttpGet]
    public Task<ActionResult<DomainsModel>> Show(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<DomainsModel>> Set(Guid memberId, Guid characterId, Guid id, DomainsFormModel form)
    {
        throw new NotImplementedException();
    }
}