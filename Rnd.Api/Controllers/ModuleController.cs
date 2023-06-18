using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/{user}/[controller]")]
public class ModulesController : ControllerBase
{
    public ModulesController(DataContext data)
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
            new Node("Modules", name)
        );
        
        return (await Data.Modules.GetAsync(tree)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user)
    {
        var tree = new Tree(
            new Node("", user)
        );
        
        return (await Data.Modules.ListAsync(tree)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, ModuleData data)
    {
        var tree = new Tree(
            new Node("", user)
        );
        
        return (await Data.Modules.CreateAsync(tree, data)).ToActionResult();
    }
    
    [HttpPut("{name}")]
    public async Task<ActionResult> Update(string user, string name, ModuleData data)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", name)
        );
        
        return (await Data.Modules.UpdateAsync(tree, data)).ToActionResult();
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(string user, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", name)
        );
        
        return (await Data.Modules.DeleteAsync(tree)).ToActionResult();
    }
}