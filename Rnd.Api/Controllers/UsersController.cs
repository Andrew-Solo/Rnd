using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/{user}/[controller]")]
public class UsersController : ControllerBase
{
    public UsersController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string user, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Users", name)
        );
        
        return (await Data.Users.GetAsync(tree)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user)
    {
        var tree = new Tree(
            new Node("", user)
        );
        
        return (await Data.Users.ListAsync(tree)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, UserData data)
    {
        var tree = new Tree(
            new Node("", user)
        );
        
        return (await Data.Users.CreateAsync(tree, data)).ToActionResult();
    }
    
    [HttpPut("{name}")]
    public async Task<ActionResult> Update(string user, string name, UserData data)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Users", name)
        );
        
        return (await Data.Users.UpdateAsync(tree, data)).ToActionResult();
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(string user, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Users", name)
        );
        
        return (await Data.Users.DeleteAsync(tree)).ToActionResult();
    }
}