using Microsoft.AspNetCore.Mvc;
using Rnd.Api.Models.Basic.Character;

namespace Rnd.Api.Controllers.Basic.Member;

[ApiController]
[Route("basic/member/{memberId:guid?}/[controller]")]
public class CharactersController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public Task<ActionResult<CharacterModel>> Get(Guid memberId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public Task<ActionResult<CharacterModel>> List(Guid memberId)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<ActionResult<CharacterModel>> Create(Guid memberId, CharacterCreateModel create)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut("{id:guid}")]
    public Task<ActionResult<CharacterModel>> Pick(Guid memberId, Guid id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<ActionResult<CharacterModel>> Edit(Guid memberId, CharacterEditModel edit)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:guid}")]
    public Task<ActionResult<CharacterModel>> Delete(Guid memberId, Guid id)
    {
        throw new NotImplementedException();
    }
}