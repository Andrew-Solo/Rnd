using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Basic.Member.Character;

[ApiController]
[Route("basic/member/{memberId:guid?}/character/{characterId:guid?}/[controller]")]
public class AdditionalController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Show()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Set()
    {
        throw new NotImplementedException();
    }
}