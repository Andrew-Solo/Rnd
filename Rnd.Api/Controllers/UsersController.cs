using Microsoft.AspNetCore.Mvc;
using Rnd.Data;
using Rnd.Models;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext db)
    {
        //DIs
        
    }

    //DIs

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User.View>> Get(Guid id)
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult> Login(string login, string password)
    {
        return Ok();
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult<bool>> Exist(Guid id)
    {
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> ValidateCreate([FromQuery] User.Form form)
    {
        return Ok();
    }
    
    [HttpGet("[action]/{id:guid}")]
    public async Task<ActionResult> ValidateUpdate(Guid id, [FromQuery] User.Form form)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<object>> Create(User.Form form)
    {
        return Ok();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<object>> Edit(Guid id, object form)
    {
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<object>> Delete(Guid id)
    {
        return Ok();
    }
}