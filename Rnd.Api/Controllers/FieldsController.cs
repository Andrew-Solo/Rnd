using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/{user}/modules/{module}/units/{unit}/[controller]")]
public class FieldsController : ControllerBase
{
    public FieldsController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> Get(string user, string module, string unit, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", unit),
            new Node("Fields", name)
        );
        
        return (await Data.Fields.GetAsync(tree)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user, string module, string unit)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", unit)
        );
        
        return (await Data.Fields.ListAsync(tree)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, string module, string unit, FieldData data)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", unit)
        );
        
        return (await Data.Fields.CreateAsync(tree, data)).ToActionResult();
    }
    
    [HttpPut("{name}")]
    public async Task<ActionResult> Update(string user, string module, string unit, string name, FieldData data)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", unit),
            new Node("Fields", name)
        );
        
        return (await Data.Fields.UpdateAsync(tree, data)).ToActionResult();
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(string user, string module, string unit, string name)
    {
        var tree = new Tree(
            new Node("", user),
            new Node("Modules", module),
            new Node("Units", unit),
            new Node("Fields", name)
        );
        
        return (await Data.Fields.DeleteAsync(tree)).ToActionResult();
    }
}