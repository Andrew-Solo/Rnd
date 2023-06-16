using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult> Get(Guid userId)
    {
        return (await Data.Users.GetAsync(userId)).ToActionResult();
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string name)
    {
        return (await Data.Users.GetAsync(name)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> Get(string login, string password)
    {
        return (await Data.Users.GetAsync(login, password)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(ExpandoObject data)
    {
        return (await Data.Users.CreateAsync(data)).ToActionResult();
    }
    
    [HttpPut("{userId:guid}")]
    public async Task<ActionResult> Update(Guid userId, ExpandoObject data)
    {
        return (await Data.Users.UpdateAsync(userId, data)).ToActionResult();
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        return (await Data.Users.DeleteAsync(userId)).ToActionResult();
    }
}