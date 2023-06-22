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
    
    [HttpGet("{value}")]
    public async Task<ActionResult> Get(string user, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(value);
        return (await Data.Modules.GetAsync(request)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user)
    {
        var request = Rnd.Data.Request.Create(user);
        return (await Data.Modules.ListAsync(request)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, ModuleData data)
    {
        var request = Rnd.Data.Request.Create(user);
        return (await Data.Modules.CreateAsync(request, data)).ToActionResult();
    }
    
    [HttpPut("{value}")]
    public async Task<ActionResult> Update(string user, string value, ModuleData data)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(value);
        return (await Data.Modules.UpdateAsync(request, data)).ToActionResult();
    }
    
    [HttpDelete("{value}")]
    public async Task<ActionResult> Delete(string user, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(value);
        return (await Data.Modules.DeleteAsync(request)).ToActionResult();
    }
}