using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/user/{userId:guid}/[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }
    
    [HttpGet("@first")]
    public async Task<ActionResult> Get(Guid userId)
    {
        return (await Data.Users.GetAsync(userId)).ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(Guid userId, Guid id)
    {
        return (await Data.Users.GetAsync(userId, id)).ToActionResult();
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> Get(Guid userId, string name)
    {
        return (await Data.Users.GetAsync(userId, name)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(Guid userId)
    {
        return (await Data.Users.ListAsync(userId)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Guid userId, UserData data)
    {
        return (await Data.Users.CreateAsync(userId, data)).ToActionResult();
    }
    
    [HttpPut("@first")]
    public async Task<ActionResult> Update(Guid userId, UserData data)
    {
        return (await Data.Users.UpdateAsync(userId, data)).ToActionResult();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid userId, Guid id, UserData data)
    {
        return (await Data.Users.UpdateAsync(userId, id, data)).ToActionResult();
    }
    
    [HttpPut("{name}")]
    public async Task<ActionResult> Update(Guid userId, string name, UserData data)
    {
        return (await Data.Users.UpdateAsync(userId, name, data)).ToActionResult();
    }
    
    [HttpDelete("@first")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        return (await Data.Users.DeleteAsync(userId)).ToActionResult();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid userId, Guid id)
    {
        return (await Data.Users.DeleteAsync(userId, id)).ToActionResult();
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(Guid userId, string name)
    {
        return (await Data.Users.DeleteAsync(userId, name)).ToActionResult();
    }
}