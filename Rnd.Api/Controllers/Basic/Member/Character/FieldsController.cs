using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Client.Models.Basic.Field;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/members/{memberId:guid}/characters/{characterId:guid}/[controller]")]
public class FieldsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<FieldModel>> Get(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<List<FieldModel>>> List(Guid memberId, Guid characterId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<FieldModel>> Add(Guid memberId, Guid characterId, FieldFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<FieldModel>> Edit(Guid memberId, Guid characterId, Guid id, FieldFormModel form)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<FieldModel>> Remove(Guid memberId, Guid characterId, Guid id)
    {
        throw new NotImplementedException();
    }
}