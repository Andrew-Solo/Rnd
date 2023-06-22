using Microsoft.AspNetCore.Mvc;
using Rnd.Data;
using Path = Rnd.Data.Path;

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
    
    [HttpGet("{value}")]
    public async Task<ActionResult> Get(string user, string module, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(value);
        return (await Data.Units.GetAsync(request)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user, string module)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module);
        return (await Data.Units.ListAsync(request)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, string module, UnitData data)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module);
        return (await Data.Units.CreateAsync(request, data)).ToActionResult();
    }
    
    [HttpPut("{value}")]
    public async Task<ActionResult> Update(string user, string module, string value, UnitData data)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(value);
        return (await Data.Units.UpdateAsync(request, data)).ToActionResult();
    }
    
    [HttpDelete("{value}")]
    public async Task<ActionResult> Delete(string user, string module, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(value);
        return (await Data.Units.DeleteAsync(request)).ToActionResult();
    }
}