using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/{user}/modules/{module}/[controller]")]
public class UnitsController : ControllerBase
{
    public UnitsController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string user, string module, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", name)
        );
        
        return (await Data.Units.GetAsync(tree)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user, string module)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module)
        );
        
        return (await Data.Units.ListAsync(tree)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, string module, UnitData data)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module)
        );
        
        return (await Data.Units.CreateAsync(tree, data)).ToActionResult();
    }
    
    [HttpPut("{name}")]
    public async Task<ActionResult> Update(string user, string module, string name, UnitData data)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", name)
        );
        
        return (await Data.Units.UpdateAsync(tree, data)).ToActionResult();
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(string user, string module, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", name)
        );
        
        return (await Data.Units.DeleteAsync(tree)).ToActionResult();
    }
}