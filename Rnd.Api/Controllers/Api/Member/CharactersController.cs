using Microsoft.AspNetCore.Mvc;

namespace Rnd.Api.Controllers.Api.Member;

[ApiController]
[Route("api/member/{memberId:guid?}/[controller]/[action]")]
public class CharactersController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> List()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public Task<IActionResult> Create()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Pick()
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public Task<IActionResult> Edit()
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public Task<IActionResult> Delete()
    {
        throw new NotImplementedException();
    }
}