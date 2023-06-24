using Microsoft.AspNetCore.Mvc;
using Rnd.Data;
using Path = Rnd.Data.Path;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/{user}/[controller]/main")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }
    
    [HttpGet("{value}")]
    public async Task<ActionResult> Get(string user, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithUsers(value);
        return (await Data.Users.GetAsync(request)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user)
    {
        var request = Rnd.Data.Request.Create(user);
        return (await Data.Users.ListAsync(request)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, UserData data)
    {
        var request = Rnd.Data.Request.Create(user);
        return (await Data.Users.CreateAsync(request, data)).ToActionResult();
    }
    
    [HttpPut("{value}")]
    public async Task<ActionResult> Update(string user, string value, UserData data)
    {
        var request = Rnd.Data.Request.Create(user).WithUsers(value);
        return (await Data.Users.UpdateAsync(request, data)).ToActionResult();
    }
    
    [HttpDelete("{value}")]
    public async Task<ActionResult> Delete(string user, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithUsers(value);
        return (await Data.Users.DeleteAsync(request)).ToActionResult();
    }
}