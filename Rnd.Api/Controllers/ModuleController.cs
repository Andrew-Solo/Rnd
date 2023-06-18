using Microsoft.AspNetCore.Mvc;
using Rnd.Data;

namespace Rnd.Api.Controllers;

[ApiController]
[Route("/{userId:guid}/[controller]")]
public class ModulesController : ControllerBase
{
    public ModulesController(DataContext data)
    {
        //DIs
        Data = data;
    }

    //DIs
    public DataContext Data { get; }
    
    [HttpGet("@first")]
    public async Task<ActionResult> Get(Guid userId)
    {
        return (await Data.Modules.GetAsync(userId)).ToActionResult();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> Get(Guid userId, Guid id)
    {
        return (await Data.Modules.GetAsync(userId, id)).ToActionResult();
    }
    
    [HttpGet("{name}")]
    public async Task<ActionResult> Get(Guid userId, string name)
    {
        return (await Data.Modules.GetAsync(userId, name)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(Guid userId)
    {
        return (await Data.Modules.ListAsync(userId)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Guid userId, ModuleData data)
    {
        return (await Data.Modules.CreateAsync(userId, data)).ToActionResult();
    }
    
    [HttpPut("@first")]
    public async Task<ActionResult> Update(Guid userId, ModuleData data)
    {
        return (await Data.Modules.UpdateAsync(userId, data)).ToActionResult();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid userId, Guid id, ModuleData data)
    {
        return (await Data.Modules.UpdateAsync(userId, id, data)).ToActionResult();
    }
    
    [HttpPut("{name}")]
    public async Task<ActionResult> Update(Guid userId, string name, ModuleData data)
    {
        return (await Data.Modules.UpdateAsync(userId, name, data)).ToActionResult();
    }
    
    [HttpDelete("@first")]
    public async Task<ActionResult> Delete(Guid userId)
    {
        return (await Data.Modules.DeleteAsync(userId)).ToActionResult();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid userId, Guid id)
    {
        return (await Data.Modules.DeleteAsync(userId, id)).ToActionResult();
    }
    
    [HttpDelete("{name}")]
    public async Task<ActionResult> Delete(Guid userId, string name)
    {
        return (await Data.Modules.DeleteAsync(userId, name)).ToActionResult();
    }
}