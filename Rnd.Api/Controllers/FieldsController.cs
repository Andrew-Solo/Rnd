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
    
    [HttpGet("{value}")]
    public async Task<ActionResult> Get(string user, string module, string unit, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(unit).WithFields(value);
        return (await Data.Fields.GetAsync(request)).ToActionResult();
    }

    [HttpGet]
    public async Task<ActionResult> List(string user, string module, string unit)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(unit);
        return (await Data.Fields.ListAsync(request)).ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult> Create(string user, string module, string unit, FieldData data)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(unit);
        return (await Data.Fields.CreateAsync(request, data)).ToActionResult();
    }
    
    [HttpPut("{value}")]
    public async Task<ActionResult> Update(string user, string module, string unit, string value, FieldData data)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(unit).WithFields(value);
        return (await Data.Fields.UpdateAsync(request, data)).ToActionResult();
    }
    
    [HttpDelete("{value}")]
    public async Task<ActionResult> Delete(string user, string module, string unit, string value)
    {
        var request = Rnd.Data.Request.Create(user).WithModules(module).WithUnits(unit).WithFields(value);
        return (await Data.Fields.DeleteAsync(request)).ToActionResult();
    }
}